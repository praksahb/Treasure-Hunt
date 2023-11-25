using TreasureHunt.Player;
using UnityEngine;

namespace TreasureHunt.Interactions
{
    public class KeyBehaviour : MonoBehaviour, IInteractable
    {
        private KeyType keyType;
        [SerializeField] private AudioSource sfxSource;

        public void SetKeyType(KeyType keyType)
        {
            this.keyType = keyType;
        }

        public void Interact(PlayerController player)
        {
            //Sounds.SoundManager.Instance.PlaySfx(Sounds.SfxType.CollectKey, sfxSource);
            // error - sound doesnt play from key's audio source  - playing from player script instead
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