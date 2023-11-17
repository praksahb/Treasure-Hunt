using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TreasureHunt.Enemy
{
    [System.Serializable]
    public class EnemyController
    {
        public EnemyView EnemyView { get; }
        public EnemyModel EnemyModel { get; }

        // CTOR
        public EnemyController(BaseEnemyData enemyData, FOVData enemyVisionData, Transform parent)
        {
            EnemyModel = new EnemyModel(enemyData, enemyVisionData);
            EnemyView = Object.Instantiate(enemyData.enemyPrefab, EnemyModel.SpawnPoint, Quaternion.identity, parent);
            EnemyView.EnemyController = this;

            SetKeyValue();
        }

        // Set held key type value
        private void SetKeyValue()
        {
            EnemyView.Key.SetKeyType(EnemyModel.KeyType);
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
        public void DrawFieldOfView()
        {
            int stepCount = Mathf.RoundToInt(EnemyModel.ViewAngle * EnemyModel.MeshResolution);
            float stepAngleSize = EnemyModel.ViewAngle / stepCount;
            List<Vector3> viewPoints = new List<Vector3>();
            for (int i = 0; i < stepCount; i++)
            {
                float angle = EnemyView.transform.eulerAngles.y - EnemyModel.ViewAngle / 2 + stepAngleSize * i;
                ViewCastInfo newViewCast = ViewCast(angle);
                viewPoints.Add(newViewCast.point);
            }

            int vertexCount = viewPoints.Count + 1;
            Vector3[] vertices = new Vector3[vertexCount];
            int[] triangles = new int[(vertexCount - 2) * 3];

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

        private void FieldOfViewCheck()
        {
            // Only one player so rangeCheck size is 1
            Collider[] rangeChecks = new Collider[1];
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
                        // Found player
                        if (target.GetComponent<Player.PlayerView>())
                        {
                            Debug.Log("Player Caught");
                        }
                    }
                }
            }
        }

        // Used for drawing field of view
        private ViewCastInfo ViewCast(float globalAngle)
        {
            Vector3 dir = DirFromAngle(globalAngle, true);
            RaycastHit hit;

            if (Physics.Raycast(EnemyView.transform.position, dir, out hit, EnemyModel.ViewRadius, EnemyModel.ObstructionMask))
            {
                return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
            }
            else
            {
                return new ViewCastInfo(false, EnemyView.transform.position + dir * EnemyModel.ViewRadius, EnemyModel.ViewRadius, globalAngle);
            }
        }

        private Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
        {
            if (!angleIsGlobal)
            {
                angleInDegrees += EnemyView.transform.eulerAngles.y;
            }
            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }
    }

    // helper class/struct for getting ray info 
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
