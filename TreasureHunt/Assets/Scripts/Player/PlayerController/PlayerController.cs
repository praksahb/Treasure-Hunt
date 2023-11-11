using System.Collections;
using TreasureHunt.Interactions;
using UnityEngine;

namespace TreasureHunt.Player
{
    public class PlayerController
    {
        // Private Member Functions 
        private void TakeDamage(int damage)
        {
            PlayerModel.Health.CurrentHealth -= damage;
            PlayerView.SetHealth(PlayerModel.Health.CurrentHealth);
            if (PlayerModel.Health.CurrentHealth == 0)
            {
                // game Over.
                PlayerView.GameOver("Player Died");
            }
        }

        // Public Properties
        public PlayerView PlayerView { get; }
        public PlayerModel PlayerModel { get; }

        // Constructors
        public PlayerController(PlayerModel playerModel, PlayerView playerPrefab)
        {
            PlayerModel = playerModel;
            PlayerView = UnityEngine.Object.Instantiate(playerPrefab);
            PlayerView.PlayerController = this;

            // Set health UI
            PlayerView.SetMaxHealth(PlayerModel.Health.MaxHealth);
        }

        public PlayerController(PlayerModel playerModel, PlayerView playerPrefab, GameObject mainCamera)
        {
            PlayerModel = playerModel;
            PlayerView = UnityEngine.Object.Instantiate(playerPrefab);
            PlayerView.PlayerController = this;

            // Set health UI
            PlayerView.SetMaxHealth(PlayerModel.Health.MaxHealth);
            PlayerView.SetHealth(PlayerModel.Health.CurrentHealth);
            // Set Values for FirstPersonController
            PlayerView.SetFPSControllerValues(PlayerModel.MoveSpeed, PlayerModel.SprintSpeed, mainCamera);

        }

        // Public Member Functions

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

        // Coroutine for taking burn damage overtime
        public IEnumerator BurnDamage(int damagePerSecond, float damageTimeInterval)
        {
            WaitForSeconds waitTime = new WaitForSeconds(damageTimeInterval);
            while (PlayerModel.IsTakingDamage)
            {
                TakeDamage(damagePerSecond);
                yield return waitTime;
            }
        }


        // Collect chest/treasure
        public void CollectChestItem()
        {
            PlayerModel.TreasureCollected = true;
            PlayerView.GameWon();
        }
    }
}
