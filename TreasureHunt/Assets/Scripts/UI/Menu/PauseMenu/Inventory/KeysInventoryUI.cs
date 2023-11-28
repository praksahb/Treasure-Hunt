using TreasureHunt.Player;
using UnityEngine;

namespace TreasureHunt
{
    // interact with collectedKey individual UI's (children) and mark it collected on ui menu

    public class KeysInventoryUI : MonoBehaviour
    {
        private PlayerController playerController;

        private void OnEnable()
        {
            CheckCollectedKeys();
        }

        private void CheckCollectedKeys()
        {
            if (playerController == null)
            {
                Debug.LogError("Player Controller not set in inventory UI");
                return;
            }

            for (int i = 0; i < transform.childCount; i++)
            {
                CollectedKey keyUI = transform.GetChild(i).GetComponent<CollectedKey>();
                if (playerController.HasKey(keyUI.KeyType))
                {
                    keyUI.KeyCollected();
                }
            }
        }

        public void SetPlayerController(PlayerController playerController)
        {
            this.playerController = playerController;
        }
    }
}
