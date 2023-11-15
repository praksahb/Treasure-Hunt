using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TreasureHunt.Enemy
{
    public class EnemyStateManager : MonoBehaviour
    {
        [SerializeField] private List<Transform> patrollingPoints;
        [SerializeField] private float idleTime;

        public EnemyController EnemyController { get; set; }
        public Vector3 TargetPoint { get { return targetPoint.position; } }

        private StateMachine stateMachine;

        private Transform targetPoint;

        private int currentIndex;
        private int totalPatrolPoints;

        private bool targetFound;

        // States to add
        // Idle state -> Search State -> Move State -> Idle State
        // Sleeping-Idle State -> Awake State / Idle State -> Move State

        private void Awake()
        {
            var navmeshAgent = GetComponent<NavMeshAgent>();
            var animator = GetComponentInChildren<Animator>();

            currentIndex = 0;
            totalPatrolPoints = patrollingPoints.Count;
            stateMachine = new StateMachine();

            var search = new SearchState(this);
            var patrol = new PatrolState(this, navmeshAgent, animator);
            var idle = new IdleState(idleTime / 2f, animator);

            var turn = new TurnState(animator, navmeshAgent);

            At(search, patrol, TargetFound());
            At(patrol, turn, ReachedTarget());
            At(turn, search, () => true);
            At(idle, search, MaxIdleTime());
            stateMachine.SetState(search);

            void At(IState to, IState from, Func<bool> condition) => stateMachine.AddTransition(to, from, condition);

            Func<bool> TargetFound() => () => targetFound == true;
            Func<bool> ReachedTarget() => () => Vector3.Distance(transform.position, targetPoint.position) < 1f;
            Func<bool> MaxIdleTime() => () => idle.currentIdleTime > idleTime;
        }

        private void Update()
        {
            stateMachine.Tick();
        }

        // sets patrol point from list and increment current index
        public void SetPatrolPoint()
        {
            targetPoint = patrollingPoints[currentIndex++ % totalPatrolPoints];
            targetFound = true;
        }
    }
}
