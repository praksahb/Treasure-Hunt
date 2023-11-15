using UnityEngine;

namespace TreasureHunt.Enemy
{
    public class IdleState : IState
    {
        public float currentIdleTime;

        private Animator animator;
        private float idleTime;
        private readonly int LongIdle = Animator.StringToHash("LongIdle");

        public IdleState(float idleTime, Animator animator)
        {
            this.animator = animator;
            this.idleTime = idleTime;
        }

        public void OnEnter()
        {
            currentIdleTime = 0f;
        }

        public void OnExit()
        {
            animator.SetBool(LongIdle, false);
        }

        public void Tick()
        {
            currentIdleTime += Time.deltaTime;
            if (currentIdleTime > idleTime)
            {
                animator.SetBool(LongIdle, true);
            }
        }
    }
}
