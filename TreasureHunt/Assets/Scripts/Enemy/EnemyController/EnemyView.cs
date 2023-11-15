using UnityEngine;

namespace TreasureHunt.Enemy
{
    public class EnemyView : MonoBehaviour
    {
        public Animator AnimatorController { get; set; }

        public Rigidbody Rigidbody { get; private set; }

        public EnemyController EnemyController { get; set; }

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            AnimatorController = GetComponentInChildren<Animator>();
        }
    }
}
