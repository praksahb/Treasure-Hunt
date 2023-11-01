using StarterAssets;
using TreasureHunt.Interactions;
using UnityEngine;

namespace TreasureHunt.Player
{
    public class PlayerView : MonoBehaviour
    {
        public PlayerController PlayerController { get; set; }

        private FirstPersonController firstPersonController;

        private IInteractable currentInteractable;

        private void Awake()
        {
            firstPersonController = GetComponentInChildren<FirstPersonController>();
        }

        private void OnEnable()
        {
            firstPersonController.useKeyPressed += OnUseKeyPressed;
        }

        private void OnDisable()
        {
            firstPersonController.useKeyPressed -= OnUseKeyPressed;
        }

        private void OnUseKeyPressed()
        {
            Debug.Log("Pressed Use Key.");

            currentInteractable?.Interact();
        }

        private void OnTriggerEnter(Collider other)
        {
            other.TryGetComponent(out IInteractable interactable);
            if (interactable != null)
            {
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
    }
}
