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
        public Transform PlayerCameraRootAlive => playerCameraRootAlive;
        public Transform PlayerCameraRootDead => playerCameraRootDead;
        public AudioSource PlayerAudioSource => playerAudioSource;
        public HurtAnimation HurtFeedback => hurtVisualFeedback;

        [SerializeField] private Transform playerCameraRootAlive;
        [SerializeField] private Transform playerCameraRootDead;
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
            _input.MoveEvent += Input_MoveEvent;
            _input.LookEvent += Input_LookEvent;
            _input.JumpEvent += Input_JumpEvent;
            _input.SprintEvent += Input_SprintEvent;
            _input.UseEvent += OnUseKeyPressed;

            _input.PauseEvent += Input_PauseEvent;
            _input.UnpauseAction += Input_UnpauseEvent;
        }

        private void OnDisable()
        {
            _input.MoveEvent -= Input_MoveEvent;
            _input.LookEvent -= Input_LookEvent;
            _input.JumpEvent -= Input_JumpEvent;
            _input.SprintEvent -= Input_SprintEvent;
            _input.UseEvent -= OnUseKeyPressed;

            _input.PauseEvent -= Input_PauseEvent;
            _input.UnpauseAction -= Input_UnpauseEvent;
        }

        private void OnDestroy()
        {
            _input.Cleanup();
        }

        private void Input_MoveEvent(Vector2 moveValue)
        {
            firstPersonController.OnMovement(moveValue);
        }

        private void Input_LookEvent(bool isMouse, Vector2 lookValue)
        {
            firstPersonController.OnLookCamera(isMouse, lookValue);
        }

        private void Input_JumpEvent(bool isJumpPressed)
        {
            firstPersonController.OnJumpPress(isJumpPressed);
        }

        private void Input_SprintEvent(bool isSprintPressed)
        {
            firstPersonController.OnSprintPress(isSprintPressed);
        }

        private void Input_UnpauseEvent()
        {
            LockCursor();
        }

        private void Input_PauseEvent()
        {
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

        public void SetFPSControllerValues(float moveSpeed, float sprintSpeed)
        {
            firstPersonController.SetValues(moveSpeed, sprintSpeed, playerAudioSource);
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
                SetInteractableText(InteractionType.ClearData);
            }
        }

        // Interaction UI related

        public void SetInteractableText(InteractionType interaction)
        {
            if (interaction == InteractionType.ClearData)
            {
                interactableUI.gameObject.SetActive(false);
            }
            else
            {
                interactableUI.gameObject.SetActive(true);
                interactableUI.SetupInteractionUI(interaction);
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
            UnlockCursor();
        }

        // trigger game won action
        public void GameWon()
        {
            _input.GameWon?.Invoke();
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

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (hit.gameObject.name != "Wall(Clone)")
            {
                //Debug.Log(hit.gameObject.name);
            }
        }
    }
}
