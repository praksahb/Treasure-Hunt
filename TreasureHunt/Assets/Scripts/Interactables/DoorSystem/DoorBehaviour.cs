using System.Collections;
using TreasureHunt.Player;
using TreasureHunt.Sounds;
using UnityEngine;

namespace TreasureHunt.Interactions
{
    public class DoorBehaviour : MonoBehaviour, IInteractable
    {
        // public property to modify door state from outside (door manager)
        public bool SetDoorLockState { get { return isLocked; } set { isLocked = value; } }

        // components
        private Animator anim;
        private AudioSource sfxSource;

        // variables
        private KeyType requiredKey;
        private bool isOpen;
        private bool isLocked;
        private bool isInteractable;

        private SoundManager soundInstance;

        private void Awake()
        {
            soundInstance = SoundManager.Instance;
            sfxSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            anim = GetComponentInChildren<Animator>();
            isOpen = false;
            isInteractable = true;
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

                if (!isOpen)
                {
                    soundInstance.PlaySfx(Sounds.SfxType.DoorClosed, sfxSource);
                }
                else
                {
                    soundInstance.PlaySfx(Sounds.SfxType.DoorOpen, sfxSource);
                }

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
                    soundInstance.PlaySfx(Sounds.SfxType.DoorClosed, sfxSource);
                }
            }
        }

        public void SetRequiredKey(KeyType keyType)
        {
            requiredKey = keyType;
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
                    soundInstance.PlaySfx(SfxType.DoorLocked, sfxSource);
                    player.PlayerView.SetInteractableText(InteractionType.DoorLocked);
                    return;
                }
            }
            OpenCloseDoor(player);
        }

        public void UIFeedback(PlayerController player)
        {
            // Send UI message for user interaction
            if (!isOpen)
            {
                player.PlayerView.SetInteractableText(InteractionType.DoorOpen);
            }
            else
            {
                player.PlayerView.SetInteractableText(InteractionType.DoorClose);
            }
        }
    }
}
