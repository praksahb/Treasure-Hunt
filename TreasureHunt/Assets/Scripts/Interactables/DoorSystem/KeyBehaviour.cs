using TreasureHunt.Player;
using UnityEngine;

namespace TreasureHunt.Interactions
{
    public class KeyBehaviour : MonoBehaviour, IInteractable
    {
        private KeyType keyType;

        public void SetKeyType(KeyType keyType)
        {
            this.keyType = keyType;
        }

        public void Interact(PlayerController player)
        {
            player.CollectKey(keyType);
            gameObject.SetActive(false);
            player.PlayerView.SetInteractableText();
        }

        public void UIFeedback(PlayerController player)
        {
            player.PlayerView.SetInteractableText("Collect Key");
        }
    }
}