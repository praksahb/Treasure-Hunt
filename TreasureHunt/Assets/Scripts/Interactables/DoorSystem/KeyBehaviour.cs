using TreasureHunt.Interactions;
using TreasureHunt.Player;
using UnityEngine;

namespace TreasureHunt
{
    public class KeyBehaviour : MonoBehaviour, IInteractable
    {
        [SerializeField] private KeyType keyType;

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