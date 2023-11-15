namespace TreasureHunt.Enemy

{
    [System.Serializable]
    public class EnemyController
    {
        public EnemyView EnemyView { get; set; }
        public EnemyModel EnemyModel { get; set; }

        public EnemyController(EnemyView enemyViewPrefab, EnemyModel enemyModel)
        {
            EnemyModel = enemyModel;
            EnemyView = UnityEngine.Object.Instantiate(enemyViewPrefab, EnemyModel.SpawnPoint, UnityEngine.Quaternion.identity);
            EnemyView.EnemyController = this;
            UnityEngine.Debug.Log("Check");
        }
    }
}
