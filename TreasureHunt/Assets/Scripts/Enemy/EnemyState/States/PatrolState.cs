using UnityEngine;
using UnityEngine.AI;

namespace TreasureHunt.Enemy
{
    public class PatrolState : IState
    {
        private EnemyStateManager enemyStateManager;
        private readonly NavMeshAgent navmeshAgent;
        private readonly Animator animator;

        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");

        private float waitTime;

        public PatrolState(EnemyStateManager enemyStateManager, NavMeshAgent navMeshAgent, Animator animator)
        {
            this.enemyStateManager = enemyStateManager;
            this.navmeshAgent = navMeshAgent;
            this.animator = animator;
        }

        public void OnEnter()
        {
            waitTime = 2f;
            navmeshAgent.enabled = true;
            navmeshAgent.SetDestination(enemyStateManager.TargetPoint);
            animator.SetFloat(Speed, 1f);
            animator.SetBool(IsWalking, true);

            navmeshAgent.updateRotation = true;

        }

        public void OnExit()
        {
            navmeshAgent.enabled = false;
            animator.SetFloat(Speed, 0f);
            animator.SetBool(IsWalking, false);
            navmeshAgent.updateRotation = false;
        }

        public void Tick()
        {

        }
    }
}
