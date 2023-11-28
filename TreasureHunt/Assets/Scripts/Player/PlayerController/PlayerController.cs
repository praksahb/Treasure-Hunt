using System;
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
                PlayerView.GameOver("Player Died");
                PlayerDied?.Invoke();
            }
        }

        // public action for invoking virtual camera change
        public Action PlayerDied;

        // Public Properties
        public PlayerView PlayerView { get; }
        public PlayerModel PlayerModel { get; }

        // Constructor

        public PlayerController(PlayerModel playerModel, PlayerView playerPrefab, Vector2 spawnPoint)
        {
            PlayerModel = playerModel;
            PlayerView = Object.Instantiate(playerPrefab, spawnPoint, Quaternion.identity);
            PlayerView.PlayerController = this;

            // Set health UI
            PlayerView.SetMaxHealth(PlayerModel.Health.MaxHealth);
            PlayerView.SetHealth(PlayerModel.Health.CurrentHealth);
            // Set Values for FirstPersonController
            PlayerView.SetFPSControllerValues(PlayerModel.MoveSpeed, PlayerModel.SprintSpeed);

        }

        // Public Member Functions

        public Transform GetFollowTarget_Alive()
        {
            return PlayerView.PlayerCameraRootAlive;
        }

        public Transform GetFollowTarget_Dead()
        {
            return PlayerView.PlayerCameraRootDead;
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

        // reduces health value
        // plays hurt sound effect 
        // plays hurt animation crossfade
        public IEnumerator BurnDamage(int damagePerSecond, float damageTimeInterval)
        {
            WaitForSeconds waitTime = new WaitForSeconds(damageTimeInterval);
            while (PlayerModel.IsTakingDamage)
            {
                TakeDamage(damagePerSecond);
                Sounds.SoundManager.Instance.PlaySfx(Sounds.SfxType.TakeDamage, PlayerView.PlayerAudioSource);
                PlayerView.HurtFeedback.Hurt_Start();
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
