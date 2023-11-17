using UnityEngine;
using UnityEngine.AI;

namespace TreasureHunt.Enemy
{
    public class PatrolState : IState
    {
        private readonly EnemyStateManager enemyStateManager;
        private readonly NavMeshAgent navmeshAgent;
        private readonly Animator animator;

        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");

        private Vector3 previousPosition;
        private float distance;

        private float DistanceTravelled()
        {
            float distanceTravelled = (navmeshAgent.transform.position - previousPosition).sqrMagnitude;
            return Mathf.Sqrt(distanceTravelled);
        }

        public PatrolState(EnemyStateManager enemyStateManager, NavMeshAgent navMeshAgent, Animator animator)
        {
            this.enemyStateManager = enemyStateManager;
            this.navmeshAgent = navMeshAgent;
            this.animator = animator;
        }

        public void OnEnter()
        {
            navmeshAgent.enabled = true;
            navmeshAgent.SetDestination(enemyStateManager.TargetPoint);
            animator.SetFloat(Speed, 1f);
            animator.SetBool(IsWalking, true);
            navmeshAgent.updateRotation = true;
            previousPosition = navmeshAgent.transform.position;
            distance = 0f;
            enemyStateManager.ResetTargetBool();
        }

        public void OnExit()
        {
            navmeshAgent.enabled = false;
            animator.SetFloat(Speed, 0f);
            animator.SetBool(IsWalking, false);
            navmeshAgent.updateRotation = false;
            distance += DistanceTravelled();
            enemyStateManager.TotalDistanceTravelled += distance;
            Debug.Log("Dist: " + enemyStateManager.TotalDistanceTravelled);
        }

        public void Tick()
        {
            distance += DistanceTravelled();
            previousPosition = navmeshAgent.transform.position;
        }
    }
}
