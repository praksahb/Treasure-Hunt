using System.Collections;
using TreasureHunt.Player;
using TreasureHunt.Sounds;
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

            TrimDoorCloseSound();
        }


        private void TrimDoorCloseSound()
        {
            // Assuming you have a reference to the SoundManager and the animation clip name is "DoorClose"
            float doorCloseClipLength = GetAnimationClipLength("CloseFromIn");

            if (!float.IsNaN(doorCloseClipLength) && SoundManager.Instance != null)
            {
                // Update the SoundSfx in the SoundManager for DoorClose
                SoundManager.Instance.UpdateSoundSfx(SfxType.DoorClosed, doorCloseClipLength);
            }
        }

        private float GetAnimationClipLength(string clipName)
        {
            foreach (AnimationClip clip in anim.runtimeAnimatorController.animationClips)
            {
                if (clip.name == clipName)
                {
                    return clip.length;
                }
            }

            Debug.LogError("Animation Clip '" + clipName + "' not found in the Animator Controller!");
            return float.NaN;
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
                    SoundManager.Instance.PlaySfx(Sounds.SfxType.DoorClosed);
                }
                else
                {
                    SoundManager.Instance.PlaySfx(Sounds.SfxType.DoorOpen);
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
                    SoundManager.Instance.PlaySfx(Sounds.SfxType.DoorClosed);
                }
            }
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
