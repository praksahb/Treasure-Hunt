using System.Collections;
using System.Collections.Generic;
using TreasureHunt.Assets.Scripts.Managers;
using UnityEngine;

namespace TreasureHunt
{
    public class FireTrapBehaviour : MonoBehaviour
    {
        [SerializeField] private Transform flameSpawnPoint;

        private ParticleSystem flameParticles;
        private CapsuleCollider capsuleCollider;
        private List<IDamageable> damageableObjects;

        private WaitForSeconds waitTime;
        private WaitForSeconds flameTime;

        private int damagePerSecond;
        private float timeBetweenFlames;
        private float damageTimeInterval;

        private void Awake()
        {
            capsuleCollider = GetComponentInChildren<CapsuleCollider>();
            damageableObjects = new List<IDamageable>();
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

                // Stops any damage-taking object(Player) not exited trigger collider
                foreach (IDamageable damageable in damageableObjects)
                {
                    damageable.StopDamage();
                }
                damageableObjects.Clear();
            }
        }

        // collision detection  to reduce player health

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                damageableObjects.Add(damageable);
                damageable.StartDamage(damagePerSecond, damageTimeInterval);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                damageableObjects.Remove(damageable);
                damageable.StopDamage();
            }
        }

        // fire particle system transform values set using transform from flameSpawnPoint
        private void SetTransform()
        {
            flameParticles.transform.parent = transform;
            flameParticles.transform.SetPositionAndRotation(flameSpawnPoint.transform.position, flameSpawnPoint.transform.rotation);
        }

        // PUBLIC METHOD

        // assign/ Set values required for trap

        public void SetDataValues(TrapData fireTrap)
        {
            flameParticles = fireTrap.flameParticle;
            SetTransform();

            timeBetweenFlames = fireTrap.timeBetweenFlames;
            damagePerSecond = fireTrap.damagePerSecond;
            damageTimeInterval = fireTrap.damageTimeInterval;

            StartFiringCoroutine();
        }
    }
}
