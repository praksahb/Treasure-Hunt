using UnityEngine;

namespace TreasureHunt.Enemy
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData")]
    public class EnemyData : ScriptableObject
    {
        public FOVData visionData;
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
        public Interactions.KeyType heldKey;
    }

    [System.Serializable]
    public class FOVData
    {
        public float timeBetweenFOVChecks;
        public float meshResolution;
        public float viewRadius;
        public float ViewAngle;
        public LayerMask targetMask, obstructionMask;
    }
}
