using UnityEngine;

namespace TreasureHunt.Enemy
{
    public class EnemyView : MonoBehaviour
    {
        // public read-only properties
        public Animator AnimatorController { get; set; }
        public EnemyController EnemyController { get; set; }
        public Mesh ViewMesh { get; private set; }

        private MeshFilter viewMeshFilter;
        private Coroutine fieldOfViewCoroutine;

        private void Awake()
        {
            viewMeshFilter = GetComponent<MeshFilter>();
            AnimatorController = GetComponent<Animator>();

            ViewMesh = new Mesh();
            ViewMesh.name = "View Mesh";
            viewMeshFilter.mesh = ViewMesh;
        }

        private void Start()
        {
            fieldOfViewCoroutine = StartCoroutine(EnemyController.FOVRoutine());
        }

        private void LateUpdate()
        {
            EnemyController.DrawFieldOfView();
        }


    }
}
