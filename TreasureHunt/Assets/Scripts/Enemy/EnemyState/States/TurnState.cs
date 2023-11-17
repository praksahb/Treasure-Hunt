using UnityEngine;

namespace TreasureHunt.Enemy
{
    public class TurnState : IState
    {
        private Animator animator;

        private static readonly int Turn180 = Animator.StringToHash("Turn180");
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");

        public TurnState(Animator animator)
        {
            this.animator = animator;
        }

        public void OnEnter()
        {
            animator.SetFloat(Speed, 1f);
            animator.SetBool(IsWalking, true);
            animator.SetTrigger(Turn180);

        }

        public void OnExit()
        {
            animator.SetFloat(Speed, 0f);
            animator.SetBool(IsWalking, false);
        }

        public void Tick()
        {
        }
    }
}
