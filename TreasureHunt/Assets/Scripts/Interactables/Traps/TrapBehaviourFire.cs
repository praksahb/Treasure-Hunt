using System.Collections;
using UnityEngine;

namespace TreasureHunt
{
    public class TrapBehaviourFire : MonoBehaviour
    {
        [SerializeField] private ParticleSystem flameParticles;
        [SerializeField] private float timeBetweenFlames;


        private WaitForSeconds waitTime;
        private WaitForSeconds flameTime;
        private CapsuleCollider capsuleCollider;

        private void Awake()
        {
            capsuleCollider = GetComponentInChildren<CapsuleCollider>();
        }

        private void Start()
        {
            float flameDuration = flameParticles.main.duration;
            flameTime = new WaitForSeconds(flameDuration);
            waitTime = new WaitForSeconds(timeBetweenFlames - flameDuration);

            StartCoroutine(RecursiveCoroutine());
        }

        private IEnumerator RecursiveCoroutine()
        {
            flameParticles.Play();

            //enable attack radius collider
            EnableCollider();


            yield return flameTime;

            DisableCollider();

            yield return waitTime;
            StartCoroutine(RecursiveCoroutine());
        }

        private void EnableCollider()
        {
            if (capsuleCollider != null)
            {
                capsuleCollider.enabled = true;
            }
        }

        private void DisableCollider()
        {
            if (capsuleCollider != null)
            {
                capsuleCollider.enabled = false;
            }
        }
    }
}
