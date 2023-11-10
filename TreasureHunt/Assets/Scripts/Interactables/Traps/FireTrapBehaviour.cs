using System.Collections;
using UnityEngine;

namespace TreasureHunt
{
    public class FireTrapBehaviour : MonoBehaviour
    {
        [SerializeField] private Transform flameSpawnPoint;
        [SerializeField] private float timeBetweenFlames;
        [SerializeField] private int damagePerSecond;
        [SerializeField] private float damageTimeInterval;

        private ParticleSystem flameParticles;

        private WaitForSeconds waitTime;
        private WaitForSeconds flameTime;
        private CapsuleCollider capsuleCollider;

        private void Awake()
        {
            capsuleCollider = GetComponentInChildren<CapsuleCollider>();
        }

        private void StartFiringCoroutine()
        {
            float flameDuration = flameParticles.main.duration;
            flameTime = new WaitForSeconds(flameDuration);
            waitTime = new WaitForSeconds(timeBetweenFlames - flameDuration);

            StartCoroutine(StartFiringRecursive());
        }

        // Coroutine for starting the fire on the trap with a gap of timeBetweenFlames - flameDuration

        private IEnumerator StartFiringRecursive()
        {
            flameParticles.Play();

            //enable attack radius collider
            EnableCollider();

            yield return flameTime;

            DisableCollider();

            yield return waitTime;
            StartCoroutine(StartFiringRecursive());
        }

        // Enable when fire particle system is played
        private void EnableCollider()
        {
            if (capsuleCollider != null)
            {
                capsuleCollider.enabled = true;
            }
        }
        // disable after the fire particle system duration is up
        private void DisableCollider()
        {
            if (capsuleCollider != null)
            {
                capsuleCollider.enabled = false;
            }
        }

        // collision detection  to reduce player health

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                damageable.StartDamage(damagePerSecond, damageTimeInterval);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                damageable.StopDamage();
            }
        }

        // assign transform values for fire particle system to launch fire at correct position and rotation
        private void SetTransform()
        {
            flameParticles.transform.parent = transform;
            flameParticles.transform.SetPositionAndRotation(flameSpawnPoint.transform.position, flameSpawnPoint.transform.rotation);
        }

        // PUBLIC METHOD

        // assign fire particle system from object pool script in particleSystemManager
        public void SetParticleSystem(ParticleSystem particleSystem)
        {
            flameParticles = particleSystem;
            SetTransform();
            StartFiringCoroutine();
        }
    }
}
