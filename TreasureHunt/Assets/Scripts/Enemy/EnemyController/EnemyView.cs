using UnityEngine;

namespace TreasureHunt.Enemy
{
    public class EnemyView : MonoBehaviour
    {
        // properties
        public Animator AnimatorController { get; set; }
        public EnemyController EnemyController { get; set; }
        // read-only properties
        public Mesh ViewMesh { get; private set; }
        public Interactions.KeyBehaviour Key { get; private set; }

        // private variables
        private MeshFilter viewMeshFilter;
        private Coroutine fieldOfViewCoroutine;

        private void Awake()
        {
            viewMeshFilter = GetComponent<MeshFilter>();
            AnimatorController = GetComponent<Animator>();
            Key = GetComponentInChildren<Interactions.KeyBehaviour>();

            ViewMesh = new Mesh();
            ViewMesh.name = "View Mesh";
            viewMeshFilter.mesh = ViewMesh;
        }

        private void Start()
        {
            if (fieldOfViewCoroutine != null)
            {
                StopCoroutine(fieldOfViewCoroutine);
            }
            fieldOfViewCoroutine = StartCoroutine(EnemyController.FOVRoutine());
        }

        private void OnDestroy()
        {
            Destroy(ViewMesh);
        }

        private void LateUpdate()
        {
            EnemyController.DrawFieldOfView();
        }
    }
}
