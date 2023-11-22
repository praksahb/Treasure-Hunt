using System.Collections.Generic;
using UnityEngine;

namespace TreasureHunt.Interactions
{
    public class TrapManager : MonoBehaviour
    {
        [SerializeField] private List<FireTrapBehaviour> fireTraps;
        [SerializeField] private ParticleSystem flameParticles;
        [SerializeField] private int flamePoolItemSize;

        private GenericPool<ParticleSystem> flamesPooler;
        private TrapData trapData;

        private void Awake()
        {
            flamesPooler = new GenericPool<ParticleSystem>(fireTraps.Count, flameParticles, transform);
            trapData = Resources.Load<TrapData>("ScriptableObjects/TrapData");
        }

        private void Start()
        {
            AssignTrapData();
        }

        private void AssignTrapData()
        {

            for (int i = 0; i < fireTraps.Count; i++)
            {
                fireTraps[i].SetReferencesAndStart(flamesPooler.GetItem(), trapData.fireTrapData);
            }
        }
    }
}