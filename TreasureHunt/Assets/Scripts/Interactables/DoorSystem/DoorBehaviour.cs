using System.Collections;
using TreasureHunt.Player;
using UnityEngine;

namespace TreasureHunt.Interactions
{
    public class DoorBehaviour : MonoBehaviour, IInteractable
    {
        private bool isOpen;
        private bool isInteractable;

        private Animator anim;

        private void Start()
        {
            anim = GetComponent<Animator>();
            isOpen = false;
            isInteractable = true;
        }

        public void Interact(PlayerController player)
        {
            OpenDoor(player);
        }

        private void OpenDoor(PlayerController player)
        {
            Debug.Log("Door open/close action");
            if (isInteractable)
            {
                isOpen = !isOpen;

                Vector3 doorTransformDirection = transform.TransformDirection(Vector3.forward);
                Vector3 playerTransformDirection = player.PlayerView.transform.position - transform.position;
                float dot = Vector3.Dot(doorTransformDirection, playerTransformDirection);
                Debug.Log("Dot Product: " + dot);
                anim.SetFloat("Dot", dot);
                anim.SetBool("IsOpen", isOpen);
                isInteractable = false;

                // if auto-closing
                StartCoroutine(AutoClose(player.PlayerView.transform));
            }
        }

        private void Animator_LockInteraction()
        {
            isInteractable = false;
        }

        private void Animator_UnlockInteraction()
        {
            isInteractable = true;
        }

        private IEnumerator AutoClose(Transform playerTransform)
        {
            while (isOpen)
            {
                yield return new WaitForSeconds(3);

                if (Vector3.Distance(transform.position, playerTransform.position) > 7)
                {
                    isOpen = false;
                    anim.SetFloat("Dot", 0);
                    anim.SetBool("IsOpen", isOpen);
                }
            }
        }

        public void UIFeedback(PlayerController player)
        {
            // Send UI message for user interaction
            if (isInteractable)
            {
                if (!isOpen)
                {
                    player.PlayerView.SetInteractableText("Open Door.");
                }
                else
                {
                    player.PlayerView.SetInteractableText("Close Door.");
                }
            }
        }
    }
}
