using System.Collections.Generic;
using TreasureHunt.Interactions;
using UnityEngine;

namespace TreasureHunt.Enemy
{
    // Storing enemy data in a common EnemyModel class
    public class EnemyModel
    {
        // for accessing individual data which can be different using a dictionary
        private Dictionary<EnemyType, BaseEnemyData> enemyDataDictionary;

        private void InitializeDictionary(ref BaseEnemyData[] enemyData)
        {
            enemyDataDictionary = new Dictionary<EnemyType, BaseEnemyData>();
            for (EnemyType enemyType = 0; enemyType < (EnemyType)enemyData.Length; enemyType++)
            {
                enemyDataDictionary.Add(enemyType, enemyData[(int)enemyType]);
            }
        }

        // common properties for all enemy with FOV
        public float FovCoroutineCheckTimer { get; private set; }
        public float MeshResolution { get; private set; }
        public float ViewRadius { get; private set; }
        public float ViewAngle { get; private set; }
        public LayerMask TargetMask { get; private set; }
        public LayerMask ObstructionMask { get; private set; }

        public EnemyModel(EnemyData enemyData)
        {
            InitializeDictionary(ref enemyData.baseEnemiesData);

            FovCoroutineCheckTimer = enemyData.visionData.timeBetweenFOVChecks;
            MeshResolution = enemyData.visionData.meshResolution;
            ViewRadius = enemyData.visionData.viewRadius;
            ViewAngle = enemyData.visionData.ViewAngle;
            TargetMask = enemyData.visionData.targetMask;
            ObstructionMask = enemyData.visionData.obstructionMask;

        }

        public EnemyView GetEnemyPrefab(EnemyType enemyType)
        {
            if (!enemyDataDictionary.ContainsKey(enemyType))
            {
                throw new System.Exception("Invalid enemyType value, enemy data not found/ does not exist.");
            }
            return enemyDataDictionary[enemyType].enemyPrefab;
        }

        public Vector3 GetSpawnPoint(EnemyType enemyType)
        {
            if (!enemyDataDictionary.ContainsKey(enemyType))
            {
                Debug.LogError("Enemy not found. Spawning at origin");
                return Vector3.zero;
            }
            return enemyDataDictionary[enemyType].patrollingPoints[0].position;
        }

        public float? GetTotalIdleTime(EnemyType enemyType)
        {
            if (!enemyDataDictionary.ContainsKey(enemyType))
            {
                return null;
            }

            return enemyDataDictionary[enemyType].totalIdleTime;
        }

        public float? GetDistanceBeforeIdle(EnemyType enemyType)
        {
            if (!enemyDataDictionary.ContainsKey(enemyType))
            {
                return null;
            }
            return enemyDataDictionary[enemyType].totalDistanceBeforeIdling;
        }

        public int GetPatrolPointsLength(EnemyType enemyType)
        {
            if (!enemyDataDictionary.ContainsKey(enemyType))
            {
                throw new System.Exception("Invalid enemyType value, enemy data not found/ does not exist.");
            }
            return enemyDataDictionary[enemyType].patrollingPoints.Length;
        }

        public Vector3 GetNextPatrolPoint(EnemyType enemyType, int index)
        {
            if (!enemyDataDictionary.ContainsKey(enemyType))
            {
                throw new System.Exception("Invalid enemyType value, enemy data not found/ does not exist.");
            }
            return enemyDataDictionary[enemyType].patrollingPoints[index].position;
        }

        public KeyType GetKeyType(EnemyType enemyType)
        {
            if (!enemyDataDictionary.ContainsKey(enemyType))
            {
                Debug.LogError("enemy data not found.");
                return KeyType.None;
            }

            return enemyDataDictionary[enemyType].heldKey;
        }
    }
}
