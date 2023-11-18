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
            enemyList = new EnemyController[enemyDataList.enemyData.Length];
            for (int i = 0; i < enemyDataList.enemyData.Length; i++)
            {
                EnemyController enemyController = new EnemyController(enemyDataList.enemyData[i], enemyDataList.visionData, transform);
                enemyList[i] = enemyController;
            }
        }
    }
}
