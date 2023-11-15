using UnityEngine;

namespace TreasureHunt.Enemy
{
    public class EnemyView : MonoBehaviour
    {
        public Animator AnimatorController { get; set; }

        public EnemyController EnemyController { get; set; }

        private void Awake()
        {
            AnimatorController = GetComponent<Animator>();
        }
    }
}
