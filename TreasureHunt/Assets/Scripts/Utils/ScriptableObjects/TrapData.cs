using UnityEngine;

namespace TreasureHunt.Interactions
{
    [CreateAssetMenu(fileName = "TrapData", menuName = "ScriptableObjects/TrapData")]
    public class TrapData : ScriptableObject
    {
        public FireTrapData fireTrapData;
    }

    [System.Serializable]
    public class FireTrapData
    {
        public float flameDuration;
        public float timeBetweenFlames;
        public int damagePerSecond;
        public float damageTimeInterval;
    }
}
