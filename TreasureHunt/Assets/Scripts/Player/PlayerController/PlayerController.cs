using TreasureHunt.Interactions;
using UnityEngine;

namespace TreasureHunt.Player
{
    public class PlayerController
    {
        public PlayerView PlayerView { get; }
        public PlayerModel PlayerModel { get; }


        public PlayerController(PlayerModel playerModel, PlayerView playerPrefab)
        {
            PlayerModel = playerModel;
            PlayerView = Object.Instantiate(playerPrefab);
            PlayerView.PlayerController = this;

            // Set health UI
            PlayerView.SetMaxHealth(PlayerModel.Health.MaxHealth);
        }

        public void TakeDamage(int damage)
        {
            PlayerModel.Health.CurrentHealth -= damage;
            PlayerView.SetHealth(PlayerModel.Health.CurrentHealth);
        }

        public Transform GetFollowCamera()
        {
            return PlayerView.PlayerCameraRoot;
        }

        public void CollectKey(KeyType keyType)
        {
            PlayerModel.KeyInventory.AddKey(keyType);
        }

        public bool HasKey(KeyType keyType)
        {
            return PlayerModel.KeyInventory.FindKey(keyType);
        }
    }
}
