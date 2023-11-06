using System.Collections;
using TreasureHunt.Player;
using UnityEngine;

namespace TreasureHunt.Interactions
{
    public class DoorBehaviour : MonoBehaviour, IInteractable
    {
        [SerializeField] private KeyType requiredKey;

        private Animator anim;

        private bool isOpen;
        private bool isLocked;
        private bool isInteractable;


        private void Start()
        {
            anim = GetComponent<Animator>();
            isOpen = false;
            isInteractable = false;
            isLocked = true;
        }

        public void Interact(PlayerController player)
        {
            if (isLocked)
            {
                if (player.HasKey(requiredKey))
                {
                    isLocked = false;
                    isInteractable = true;
                }
                else
                {
                    player.PlayerView.SetInteractableText("Locked. Find Key.");
                }
            }

            OpenCloseDoor(player);
        }


        private void OpenCloseDoor(PlayerController player)
        {
            if (isInteractable)
            {
                isOpen = !isOpen;

                Vector3 doorTransformDirection = transform.TransformDirection(Vector3.forward);
                Vector3 playerTransformDirection = player.PlayerView.transform.position - transform.position;
                float dot = Vector3.Dot(doorTransformDirection, playerTransformDirection);
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
            else
            {
                player.PlayerView.SetInteractableText("Unlock Door");
            }
        }
    }
}
