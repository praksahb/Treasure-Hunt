using StarterAssets;
using TreasureHunt.Interactions;
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
        private FirstPersonController firstPersonController;
        private IInteractable currentInteractable;

        private void Awake()
        {
            firstPersonController = GetComponentInChildren<FirstPersonController>();
            healthUI = GetComponentInChildren<HealthUI>();
        }

        private void OnEnable()
        {
            firstPersonController.useKeyPressed += OnUseKeyPressed;
        }

        private void OnDisable()
        {
            firstPersonController.useKeyPressed -= OnUseKeyPressed;
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
                interactable.UIFeedback();
                currentInteractable = interactable;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            other.TryGetComponent(out IInteractable interactable);

            if (currentInteractable == interactable)
            {
                currentInteractable = null;
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
