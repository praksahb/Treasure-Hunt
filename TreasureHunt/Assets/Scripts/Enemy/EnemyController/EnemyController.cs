using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TreasureHunt.Enemy
{
    [System.Serializable]
    public class EnemyController
    {
        public EnemyView EnemyView { get; }
        public EnemyModel EnemyModel { get; }

        public EnemyController(EnemyModel enemyModel, EnemyType enemyType, Transform parent)
        {
            // clone game object from prefab
            this.enemyType = enemyType;
            EnemyModel = enemyModel;
            EnemyView = Object.Instantiate(EnemyModel.GetEnemyPrefab(this.enemyType), EnemyModel.GetSpawnPoint(this.enemyType), Quaternion.identity, parent);
            EnemyView.EnemyController = this;

            SetKeyValue();

            // initialize values for patrol points - enemyStateManager
            currentIndex = 0;
            totalPatrolPoints = EnemyModel.GetPatrolPointsLength(this.enemyType);

            // Fov readonly values initialization
            rangeChecks = new Collider[1];
            stepCount = Mathf.RoundToInt(EnemyModel.ViewAngle * EnemyModel.MeshResolution);
            viewPoints = new Vector3[stepCount];
            vertexCount = viewPoints.Length + 1;
            vertices = new Vector3[vertexCount];
            triangles = new int[(vertexCount - 2) * 3];
        }

        //Getters for state manager

        public float GetTotalIdleTime()
        {
            float? val = EnemyModel.GetTotalIdleTime(enemyType);
            if (val.HasValue)
            {
                return (float)val;
            }
            else
            {
                // can return default value for idle time
                return 0f;
            }
        }

        public float GetDistanceBeforeIdle()
        {
            float? val = EnemyModel.GetDistanceBeforeIdle(enemyType);
            if (val.HasValue)
            {
                return (float)val;
            }
            else
            {
                // return default value
                return 0f;
            }
        }

        public Vector3 GetNextPatrolPoint()
        {
            // first index is the spawn point
            int index = ++currentIndex % totalPatrolPoints;
            return EnemyModel.GetNextPatrolPoint(enemyType, index);
        }

        // Coroutine for checking if player is inside FOV
        public IEnumerator FOVRoutine()
        {
            WaitForSeconds wait = new WaitForSeconds(EnemyModel.FovCoroutineCheckTimer);

            while (true)
            {
                yield return wait;
                FieldOfViewCheck();
            }
        }

        // Draws a triangle mesh to show FOV of enemy
        // No. of rays created per frame = stepCount
        public void DrawFieldOfView()
        {
            float stepAngleSize = EnemyModel.ViewAngle / stepCount;
            for (int i = 0; i < stepCount; i++)
            {
                float angle = EnemyView.transform.eulerAngles.y - EnemyModel.ViewAngle / 2 + stepAngleSize * i;
                ViewCastInfo newViewCast = ViewCast(angle);
                viewPoints[i] = (newViewCast.point);
            }

            vertices[0] = Vector3.zero;
            for (int i = 0; i < vertexCount - 1; i++)
            {
                vertices[i + 1] = EnemyView.transform.InverseTransformPoint(viewPoints[i]);

                if (i < vertexCount - 2)
                {
                    triangles[i * 3] = 0;
                    triangles[i * 3 + 1] = i + 1;
                    triangles[i * 3 + 2] = i + 2;
                }
            }

            EnemyView.ViewMesh.Clear();
            EnemyView.ViewMesh.vertices = vertices;
            EnemyView.ViewMesh.triangles = triangles;
            EnemyView.ViewMesh.RecalculateBounds();
        }

        // Private variables 

        private EnemyType enemyType;

        // patrol points index
        private int currentIndex = 0;
        private int totalPatrolPoints;

        // draw fov
        private readonly int stepCount;
        private readonly Vector3[] viewPoints;
        private readonly int vertexCount;
        private readonly Vector3[] vertices;
        private readonly int[] triangles;
        private Vector3 directionFromAngle;
        private RaycastHit hit;

        // fov checks
        // Only one player so rangeCheck size is 1
        private readonly Collider[] rangeChecks;

        // Set held key type value
        private void SetKeyValue()
        {
            EnemyView.Key.SetKeyType(EnemyModel.GetKeyType(enemyType));
        }

        // checks if player is within the view field
        private void FieldOfViewCheck()
        {
            int numColliders = Physics.OverlapSphereNonAlloc(EnemyView.transform.position, EnemyModel.ViewRadius, rangeChecks, EnemyModel.TargetMask);

            if (numColliders != 0)
            {
                Transform target = rangeChecks[0].transform;
                Vector3 directionToTarget = (target.position - EnemyView.transform.position).normalized;

                // vision cone defined by angle
                if (Vector3.Angle(EnemyView.transform.forward, directionToTarget) < EnemyModel.ViewAngle / 2)
                {
                    float distanceToTarget = Vector3.Distance(EnemyView.transform.position, target.position);

                    // if no obstruction
                    if (!Physics.Raycast(EnemyView.transform.position, directionToTarget, distanceToTarget, EnemyModel.ObstructionMask))
                    {
                        if (target.TryGetComponent(out IDetectable playerDetected))
                        {
                            playerDetected.Detected();
                        }
                    }
                }
            }
        }

        // calculates ray info at one angle within the viewingAngle range, 
        // used in drawing the mesh for the view zone
        private ViewCastInfo ViewCast(float globalAngle)
        {
            DirFromAngle(globalAngle, out directionFromAngle);

            // return obstructed ray length to be drawn till hit.point
            if (Physics.Raycast(EnemyView.transform.position, directionFromAngle, out hit, EnemyModel.ViewRadius, EnemyModel.ObstructionMask))
            {
                return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
            }
            else // return ray of max length equal to radius length
            {
                return new ViewCastInfo(false, EnemyView.transform.position + directionFromAngle * EnemyModel.ViewRadius, EnemyModel.ViewRadius, globalAngle);
            }
        }

        private void DirFromAngle(float angleInDegrees, out Vector3 directionFromAngle)
        {
            directionFromAngle.x = Mathf.Sin(angleInDegrees * Mathf.Deg2Rad);
            directionFromAngle.y = 0f;
            directionFromAngle.z = Mathf.Cos(angleInDegrees * Mathf.Deg2Rad);

            //return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }
    }

    // helper class/struct for getting individual ray's info 
    public struct ViewCastInfo
    {
        public bool hit;
        public Vector3 point;
        public float distance;
        public float angle;

        public ViewCastInfo(bool hit, Vector3 point, float distance, float angle)
        {
            this.hit = hit;
            this.point = point;
            this.distance = distance;
            this.angle = angle;
        }
    }
}
