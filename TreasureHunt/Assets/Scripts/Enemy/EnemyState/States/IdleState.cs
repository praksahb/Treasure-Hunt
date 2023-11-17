using UnityEngine;

namespace TreasureHunt.Enemy
{
    public class IdleState : IState
    {
        private EnemyStateManager enemyStateManager;

        public float totalIdleTime;

        public IdleState(EnemyStateManager enemyStateManager)
        {
            this.enemyStateManager = enemyStateManager;
        }

        public void OnEnter()
        {
            totalIdleTime = 0f;
            enemyStateManager.TotalDistanceTravelled = 0f;
        }

        public void OnExit()
        {
        }

        public void Tick()
        {
            totalIdleTime += Time.deltaTime;
        }
    }
}
