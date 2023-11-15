using UnityEngine;

namespace TreasureHunt.Enemy
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] EnemyData enemyDataList;

        public EnemyController[] enemyList;

        private void Awake()
        {
            InitializeEnemies();
        }

        private void InitializeEnemies()
        {
            enemyList = new EnemyController[enemyDataList.enemyData.Length];
            int i = 0;
            foreach (var enemy in enemyDataList.enemyData)
            {
                EnemyModel enemyModel = new EnemyModel(enemy);
                EnemyController enemyController = new EnemyController(enemy.enemyPrefab, enemyModel);
                enemyList[i++] = enemyController;
            }
        }
    }
}
