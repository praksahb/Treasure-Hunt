namespace TreasureHunt.Enemy

{
    public class EnemyController
    {
        public EnemyView EnemyView { get; set; }
        public EnemyModel EnemyModel { get; set; }

        public EnemyController(EnemyView enemyView, EnemyModel enemyModel)
        {
            EnemyModel = enemyModel;
            EnemyView = enemyView;

            EnemyView.EnemyController = this;
        }
    }
}
