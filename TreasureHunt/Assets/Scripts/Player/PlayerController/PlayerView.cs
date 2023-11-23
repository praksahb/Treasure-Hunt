using TreasureHunt.InputSystem;
using TreasureHunt.Interactions;
using TreasureHunt.Player.StarterAssets;
using TreasureHunt.Player.UI;
using UnityEngine;
using UnityEngine.Scripting;

namespace TreasureHunt.Player
{
    [RequiredInterface(typeof(IDamageable))]
    [RequiredInterface(typeof(IDetectable))]
    [RequireComponent(typeof(FirstPersonController), typeof(AudioSource))]
    public class PlayerView : MonoBehaviour, IDamageable, IDetectable
    {
        //  Properties
        public PlayerController PlayerController { get; set; }
        public Transform PlayerCameraRoot => playerCameraRoot;
        public AudioSource PlayerAudioSource => playerAudioSource;
        public HurtAnimation HurtFeedback => hurtVisualFeedback;

        [SerializeField] private Transform playerCameraRoot;
        [SerializeField] private UI.HealthUI healthUI;
        [SerializeField] private InteractableUI interactableUI;
        [SerializeField] private HurtAnimation hurtVisualFeedback;
        [SerializeField] private FirstPersonController firstPersonController;
        [SerializeField] private AudioSource playerAudioSource;

        private InputReader _input;
        private IInteractable currentInteractable;
        private Coroutine TakeDamageCoroutine;

        private void Awake()
        {
            _input = Resources.Load<InputReader>("InputSystem/InputReader");

            if (_input == null)
            {
                Debug.LogError("InputReader resource not found. Make sure the path is correct.");
            }

            LockCursor();
        }

        private void OnEnable()
        {
            firstPersonController.useKeyPressed += OnUseKeyPressed;
            _input.PauseEvent += Input_PauseEvent;
            _input.UnpauseEvent += Input_UnpauseEvent;
        }

        private void OnDisable()
        {
            firstPersonController.useKeyPressed -= OnUseKeyPressed;
            _input.PauseEvent -= Input_PauseEvent;
            _input.UnpauseEvent -= Input_UnpauseEvent;
        }

        private void Input_UnpauseEvent()
        {
            firstPersonController.SetPause(false);
            LockCursor();
        }

        private void Input_PauseEvent()
        {
            firstPersonController.SetPause(true);
            UnlockCursor();
        }

        private void UnlockCursor()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        private void LockCursor()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        // Set values for the FirstPersonController from playerData

        public void SetFPSControllerValues(float moveSpeed, float sprintSpeed, GameObject mainCamera)
        {
            firstPersonController.SetValues(moveSpeed, sprintSpeed, mainCamera);
        }

        // Interaction with use Key
        private void OnUseKeyPressed()
        {
            currentInteractable?.Interact(PlayerController);
        }

        private void OnTriggerEnter(Collider other)
        {
            other.TryGetComponent(out IInteractable interactable);
            if (interactable != null)
            {
                interactable.UIFeedback(PlayerController);
                currentInteractable = interactable;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            other.TryGetComponent(out IInteractable interactable);

            if (currentInteractable == interactable)
            {
                currentInteractable = null;
                SetInteractableText();
            }
        }

        // Interaction UI related

        public void SetInteractableText(string text = null)
        {
            if (text == null)
            {
                interactableUI.ClearText();
            }
            else
            {
                interactableUI.SetText(text);
            }
        }

        // Health related 

        public void SetMaxHealth(int health)
        {
            if (healthUI == null) return;
            healthUI.SetMaxHealth(health);
        }

        public void SetHealth(int health)
        {
            if (healthUI == null) return;
            healthUI.SetHealth(health);
        }

        // Detected by enemy vision
        public void Detected()
        {
            GameOver("Player Caught.");
        }

        // trigger game over action 
        public void GameOver(string reason)
        {
            _input.GameOverAction?.Invoke(reason);
            firstPersonController.enabled = false;
            UnlockCursor();
        }

        // trigger game won action
        public void GameWon()
        {
            _input.GameWon?.Invoke();
            firstPersonController.enabled = false;
            UnlockCursor();
        }

        // Taking damage from traps

        public void StartDamage(int damagePerSecond, float damageTimeInterval)
        {
            if (PlayerController.PlayerModel.Health.CurrentHealth == 0)
            {
                return;
            }
            PlayerController.PlayerModel.IsTakingDamage = true;
            if (TakeDamageCoroutine != null)
            {
                StopCoroutine(TakeDamageCoroutine);
            }
            TakeDamageCoroutine = StartCoroutine(PlayerController.BurnDamage(damagePerSecond, damageTimeInterval));
        }

        public void StopDamage()
        {
            PlayerController.PlayerModel.IsTakingDamage = false;
            if (TakeDamageCoroutine != null)
            {
                StopCoroutine(TakeDamageCoroutine);
            }
        }
    }
}
