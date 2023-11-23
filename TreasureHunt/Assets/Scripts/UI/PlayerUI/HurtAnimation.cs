using UnityEngine;

namespace TreasureHunt
{
    // starts animation of screen flashing red when player is taking damage
    [RequireComponent(typeof(Animator))]
    public class HurtAnimation : MonoBehaviour
    {
        [SerializeField] private Animator hurtTransition;
        [SerializeField] private float transitionTime;

        private static readonly int Hurt = Animator.StringToHash("Hurt");

        public void Hurt_Start()
        {
            hurtTransition.SetBool(Hurt, true);
        }
    }
}
