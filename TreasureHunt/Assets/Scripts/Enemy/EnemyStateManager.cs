using System;
using UnityEngine;
using UnityEngine.AI;

namespace TreasureHunt.Enemy
{
    public class EnemyStateManager : MonoBehaviour
    {
        public EnemyController EnemyController { get; private set; }
        public Vector3 TargetPoint { get; private set; }
        public float TotalIdleTime { get { return EnemyController.EnemyModel.TotalIdleTime; } }
        public float TotalDistanceTravelled { get; set; }

        private StateMachine stateMachine;

        private int currentIndex;
        private int totalPatrolPoints;
        private bool targetFound;

        // States to add
        // Idle state -> Search State -> Move State -> Idle State
        // Sleeping-Idle State -> Awake State / Idle State -> Move State

        private void Awake()
        {
            // get references
            NavMeshAgent navmeshAgent = GetComponent<NavMeshAgent>();
            Animator animator = GetComponentInChildren<Animator>();


            InitializeStateMachine(navmeshAgent, animator);
        }

        private void Start()
        {
            EnemyController = GetComponent<EnemyView>().EnemyController;

            // assign values 
            currentIndex = 0;
            totalPatrolPoints = EnemyController.EnemyModel.PatrolPoints.Length;
        }

        private void Update()
        {
            stateMachine.Tick();
        }

        private void InitializeStateMachine(NavMeshAgent navmeshAgent, Animator animator)
        {
            // instantiate state machine and states passing required references
            stateMachine = new StateMachine();
            var search = new SearchState(this);
            var patrol = new PatrolState(this, navmeshAgent, animator);
            var turn = new TurnState(animator);
            var idle = new IdleState(this);

            // fixed transitions from state, to  state
            At(search, patrol, TargetFound());
            At(patrol, turn, ReachedTarget());
            At(turn, search, () => true); // will result in only 180 degs turn animations
            At(idle, search, MaxIdleTime());

            // any transition - can be set from any state
            stateMachine.AddAnyTransition(idle, () => TotalDistanceTravelled > EnemyController.EnemyModel.TotalDistanceBeforeIdle);

            // Initial state
            stateMachine.SetState(search);

            void At(IState from, IState to, Func<bool> condition) => stateMachine.AddTransition(from, to, condition);

            // conditions for transition to change state from -> to
            Func<bool> TargetFound() => () => targetFound == true;
            Func<bool> ReachedTarget() => () => Vector3.Distance(transform.position, TargetPoint) < 1f;
            Func<bool> MaxIdleTime() => () => idle.totalIdleTime > TotalIdleTime;
        }

        // sets patrol point from list and increment current index
        public void SetPatrolPoint()
        {
            TargetPoint = EnemyController.EnemyModel.PatrolPoints[++currentIndex % totalPatrolPoints].position;
            targetFound = true;
        }

        public void ResetTargetBool()
        {
            targetFound = false;
        }
    }
}
