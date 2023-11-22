using UnityEngine;

namespace TreasureHunt.Enemy
{
    public class EnemyManager : MonoBehaviour
    {
        EnemyData enemyDataList;

        private EnemyController[] enemyList;

        private void Awake()
        {
            enemyDataList = Resources.Load<EnemyData>("ScriptableObjects/EnemyData");
            InitializeEnemies();
        }

        private void InitializeEnemies()
        {
            enemyList = new EnemyController[enemyDataList.baseEnemiesData.Length];
            EnemyModel enemyModel = new EnemyModel(enemyDataList);
            for (int i = 0; i < enemyDataList.baseEnemiesData.Length; i++)
            {
                EnemyController enemyController = new EnemyController(enemyModel, enemyDataList.baseEnemiesData[i].enemyType, transform);

                enemyList[i] = enemyController;
            }
        }
    }
}
