using System.Collections.Generic;
using UnityEngine;

namespace TreasureHunt.Assets.Scripts.Managers
{
    public class TrapManager : MonoBehaviour
    {
        [SerializeField] private List<FireTrapBehaviour> fireTraps;
        [SerializeField] private ParticleSystem flameParticles;
        [SerializeField] private int flamePoolItemSize;
        [SerializeField] private float timeBetweenFlames = 10f;
        [SerializeField] private int damagePerSecond = 5;
        [SerializeField] private float damageTimeInterval = 1f;

        private GenericPool<ParticleSystem> flamesPooler;

        private void Awake()
        {
            flamesPooler = new GenericPool<ParticleSystem>(flamePoolItemSize, flameParticles, transform);
        }

        private void Start()
        {
            AssignTrapData();
        }

        private void AssignTrapData()
        {
            TrapData fireTrapData = new TrapData(flamesPooler.GetItem(), damagePerSecond, timeBetweenFlames, damageTimeInterval);

            for (int i = 0; i < fireTraps.Count; i++)
            {
                fireTraps[i].SetDataValues(fireTrapData);
            }
        }
    }

    public struct TrapData
    {
        public ParticleSystem flameParticle;
        public int damagePerSecond;
        public float timeBetweenFlames;
        public float damageTimeInterval;

        public TrapData(ParticleSystem flameParticle, int damagePerSecond, float timeBetweenFlames, float damageTimeInterval)
        {
            this.flameParticle = flameParticle;
            this.damagePerSecond = damagePerSecond;
            this.timeBetweenFlames = timeBetweenFlames;
            this.damageTimeInterval = damageTimeInterval;
        }
    }
}