using System.Collections;
using UnityEngine;

namespace TreasureHunt.Interactions
{
    public class FireTrapBehaviour : MonoBehaviour
    {
        [SerializeField] private Transform flameSpawnPoint;

        private ParticleSystem flameParticles;
        private CapsuleCollider capsuleCollider;
        private AudioSource sfxSource;

        // private int numOfDamageableObjects;
        //private IDamageable[] damageableObjects; // can be a array instead of size 1 for one player 
        private IDamageable player;

        private WaitForSeconds waitTime;
        private WaitForSeconds flameTime;

        private FireTrapData fireTrapData;

        private void Awake()
        {
            sfxSource = GetComponent<AudioSource>();
            capsuleCollider = GetComponentInChildren<CapsuleCollider>();
            //numOfDamageableObjects = 1; // currently only one player can be damaged by fire
            //damageableObjects = new IDamageable[numOfDamageableObjects];
        }

        private void StartFiringCoroutine()
        {
            float flameDuration = flameParticles.main.duration;
            flameTime = new WaitForSeconds(flameDuration);
            waitTime = new WaitForSeconds(fireTrapData.timeBetweenFlames - flameDuration);

            StartCoroutine(StartFiringRecursive());
        }

        // Coroutine for starting the fire on the trap with a gap of timeBetweenFlames - flameDuration

        private IEnumerator StartFiringRecursive()
        {
            flameParticles.Play();
            Sounds.SoundManager.Instance.PlaySfx(Sounds.SfxType.Flamethrower, sfxSource);
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
                //foreach (IDamageable damageable in damageableObjects)
                //{
                //    damageable.StopDamage();
                //}
                //damageableObjects.Clear();

                player?.StopDamage();
            }
        }

        // collision detection  to reduce player health

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                //damageableObjects.Add(damageable);
                player = damageable;
                damageable.StartDamage(fireTrapData.damagePerSecond, fireTrapData.damageTimeInterval);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                //damageableObjects.Remove(damageable);
                player = null;
                damageable.StopDamage();
            }
        }

        // fire particle system transform values set using transform from flameSpawnPoint
        private void PositionFlameParticleTransform()
        {
            // flameParticles.transform.parent = transform;
            flameParticles.transform.SetPositionAndRotation(flameSpawnPoint.transform.position, flameSpawnPoint.transform.rotation);
        }

        // set time duration of flame, 
        private void SetFlamesDuration()
        {
            ParticleSystem.MainModule flamePS = flameParticles.main;
            flamePS.duration = fireTrapData.flameDuration;

            // change in child fireEmber PS
            Transform childTf = flameParticles.transform.GetChild(0);
            if (childTf.TryGetComponent(out ParticleSystem childParticle))
            {
                var childPS = childParticle.main;
                childPS.duration = fireTrapData.flameDuration;
            }
        }

        // PUBLIC METHOD

        // assign/ Set values required for trap and start firing coroutine

        public void SetReferencesAndStart(ParticleSystem flameParticle, FireTrapData fireTrapData)
        {
            this.flameParticles = flameParticle;
            this.fireTrapData = fireTrapData;

            PositionFlameParticleTransform();
            SetFlamesDuration();

            StartFiringCoroutine();
        }
    }
}
