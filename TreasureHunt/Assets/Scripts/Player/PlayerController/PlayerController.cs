using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TreasureHunt.Player
{
    public class PlayerController
    {
        // Private Member Function
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

        // Constructor

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
            Sounds.SoundManager.Instance.PlaySfx(Sounds.SfxType.CollectKey, PlayerView.PlayerAudioSource);
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
                Sounds.SoundManager.Instance.PlaySfx(Sounds.SfxType.TakeDamage, PlayerView.PlayerAudioSource);
                PlayerView.HurtFeedback.Hurt_Start();
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
