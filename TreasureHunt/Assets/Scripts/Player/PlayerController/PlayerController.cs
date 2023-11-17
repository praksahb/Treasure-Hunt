using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

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
            PlayerView = Object.Instantiate(playerPrefab);
            PlayerView.PlayerController = this;

            // Set health UI
            PlayerView.SetMaxHealth(PlayerModel.Health.MaxHealth);
        }

        public PlayerController(PlayerModel playerModel, PlayerView playerPrefab, GameObject mainCamera)
        {
            PlayerModel = playerModel;
            PlayerView = Object.Instantiate(playerPrefab);
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

        public void CollectKey(Interactions.KeyType keyType)
        {
            PlayerModel.KeyInventory.AddKey(keyType);
        }

        public bool HasKey(Interactions.KeyType keyType)
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
