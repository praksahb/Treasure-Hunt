using System;
using UnityEngine;

namespace TreasureHunt.Enemy
{
    public class EnemyModel
    {
        public Transform[] PatrolPoints { get; private set; }

        public float TotalIdleTime { get; private set; }
        public float TotalDistanceBeforeIdle { get; private set; }

        public Vector3 SpawnPoint { get; private set; }

        public EnemyModel(BaseEnemyData enemyData)
        {
            TotalIdleTime = enemyData.totalIdleTime;
            TotalDistanceBeforeIdle = enemyData.totalDistanceBeforeIdling;
            SpawnPoint = enemyData.patrollingPoints[0].position;

            PatrolPoints = new Transform[enemyData.patrollingPoints.Length];
            Array.Copy(enemyData.patrollingPoints, PatrolPoints, enemyData.patrollingPoints.Length);
        }

    }
}
