using TreasureHunt.Player;
using UnityEngine;

namespace TreasureHunt.Interactions
{
    public class TreasureChest : MonoBehaviour, IInteractable
    {
        [SerializeField] private KeyType chestKey;

        private new Animation animation;
        private AnimationState animationState;

        private bool isOpen;
        private bool isLocked;

        private void Awake()
        {
            animation = GetComponent<Animation>();
            animationState = animation[animation.clip.name];
        }

        private void Start()
        {
            isLocked = true;
            isOpen = false;
        }

        private void OpenChest(PlayerController player)
        {

            if (!isOpen && !isLocked)
            {
                animationState.speed = 1.0f;
                isOpen = true;
                animation.Play();
                player.CollectChestItem();
                player.PlayerView.SetInteractableText(InteractionType.ClearData);
            }
        }

        private void CloseChest()
        {
            if (isOpen)
            {
                isOpen = false;
                animationState.time = animationState.length;
                animationState.speed = -1f;
                animation.Play();
            }
        }

        public void Interact(PlayerController player)
        {
            if (isLocked)
            {
                if (player.HasKey(chestKey))
                {
                    isLocked = false;
                }
                else
                {
                    player.PlayerView.SetInteractableText(InteractionType.TreasureLocked);
                }
            }
            OpenChest(player);
        }

        public void UIFeedback(PlayerController player)
        {
            player.PlayerView.SetInteractableText(InteractionType.TreasureCollect);
        }
    }
}
