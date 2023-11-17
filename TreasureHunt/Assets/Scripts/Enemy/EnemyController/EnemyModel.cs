using System;
using UnityEngine;

namespace TreasureHunt.Enemy
{
    public class EnemyModel
    {
        public Vector3 SpawnPoint { get; private set; }

        // used in StateManager
        public Transform[] PatrolPoints { get; private set; }
        public float TotalIdleTime { get; private set; }
        public float TotalDistanceBeforeIdle { get; private set; }

        // Field Of View values
        public float FovCoroutineCheckTimer { get; private set; }
        public float MeshResolution { get; private set; }
        public float ViewRadius { get; private set; }
        public float ViewAngle { get; private set; }
        public LayerMask TargetMask { get; private set; }
        public LayerMask ObstructionMask { get; private set; }

        public EnemyModel(BaseEnemyData enemyData, FOVData fovData)
        {
            SpawnPoint = enemyData.patrollingPoints[0].position;
            PatrolPoints = new Transform[enemyData.patrollingPoints.Length];
            Array.Copy(enemyData.patrollingPoints, PatrolPoints, enemyData.patrollingPoints.Length);
            TotalIdleTime = enemyData.totalIdleTime;
            TotalDistanceBeforeIdle = enemyData.totalDistanceBeforeIdling;

            FovCoroutineCheckTimer = fovData.timeBetweenFOVChecks;
            MeshResolution = fovData.meshResolution;
            ViewRadius = fovData.viewRadius;
            ViewAngle = fovData.ViewAngle;
            TargetMask = fovData.targetMask;
            ObstructionMask = fovData.obstructionMask;
        }

    }
}
