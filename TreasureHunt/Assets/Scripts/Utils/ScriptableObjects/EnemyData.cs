using UnityEngine;

namespace TreasureHunt.Enemy
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData")]
    public class EnemyData : ScriptableObject
    {
        public BaseEnemyData[] enemyData;
    }

    [System.Serializable]
    public class BaseEnemyData
    {
        public EnemyType enemyType;
        public EnemyView enemyPrefab;
        public Transform[] patrollingPoints;
        public float totalIdleTime;
        public float totalDistanceBeforeIdling;
    }
}
