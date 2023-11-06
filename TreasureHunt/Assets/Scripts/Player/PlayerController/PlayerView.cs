using StarterAssets;
using TreasureHunt.Interactions;
using TreasureHunt.Player.UI;
using UnityEngine;

namespace TreasureHunt.Player
{
    public class PlayerView : MonoBehaviour
    {
        //  Properties
        public PlayerController PlayerController { get; set; }
        public Transform PlayerCameraRoot => playerCameraRoot;

        [SerializeField] private Transform playerCameraRoot;

        private HealthUI healthUI;
        private InteractableUI interactableUI;
        private FirstPersonController firstPersonController;
        private IInteractable currentInteractable;

        private void Awake()
        {
            firstPersonController = GetComponent<FirstPersonController>();
            healthUI = GetComponentInChildren<HealthUI>();
            interactableUI = GetComponentInChildren<InteractableUI>();
        }

        private void OnEnable()
        {
            firstPersonController.useKeyPressed += OnUseKeyPressed;
        }

        private void OnDisable()
        {
            firstPersonController.useKeyPressed -= OnUseKeyPressed;
        }

        // Set values for the FirstPersonController from playerData

        public void SetFPSControllerValues(float moveSpeed, float sprintSpeed, GameObject mainCamera)
        {
            firstPersonController.SetValues(moveSpeed, sprintSpeed, mainCamera);
        }

        // Interaction with use Key
        private void OnUseKeyPressed()
        {
            Debug.Log("Pressed Use Key.");

            currentInteractable?.Interact(PlayerController);
        }

        private void OnTriggerEnter(Collider other)
        {
            other.TryGetComponent(out IInteractable interactable);
            if (interactable != null)
            {
                Debug.Log("check");
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
            healthUI.SetMaxHealth(health);
        }

        public void SetHealth(int health)
        {
            healthUI.SetHealth(health);
        }
    }
}
