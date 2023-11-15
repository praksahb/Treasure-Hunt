namespace TreasureHunt.Enemy
{
    public class SearchState : IState
    {
        private EnemyStateManager enemyStateManager;


        public SearchState(EnemyStateManager enemyStateManager)
        {
            this.enemyStateManager = enemyStateManager;
        }

        public void OnEnter()
        {

        }

        public void OnExit()
        {

        }

        public void Tick()
        {
            enemyStateManager.SetPatrolPoint();
        }
    }
}
