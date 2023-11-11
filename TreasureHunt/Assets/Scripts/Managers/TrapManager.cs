using System.Collections.Generic;
using UnityEngine;

namespace TreasureHunt.Assets.Scripts.Managers
{
    public class TrapManager : MonoBehaviour
    {
        [SerializeField] private List<FireTrapBehaviour> fireTraps;
        [SerializeField] private ParticleSystem flameParticles;
        [SerializeField] private int flamePoolItems;
        private GenericPool<ParticleSystem> flamesPooler;

        private void Awake()
        {
            flamesPooler = new GenericPool<ParticleSystem>(flamePoolItems, flameParticles, transform);
            AssignParticleSystem();
        }

        private void Start()
        {

        }

        private void AssignParticleSystem()
        {
            for (int i = 0; i < fireTraps.Count; i++)
            {
                fireTraps[i].SetParticleSystem(flamesPooler.GetItem());
            }
        }
    }
}