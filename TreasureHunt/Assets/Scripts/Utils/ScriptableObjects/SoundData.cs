using UnityEngine;

namespace TreasureHunt.Sounds
{
    [CreateAssetMenu(fileName = "SoundData", menuName = "ScriptableObjects/SoundData")]
    public class SoundData : ScriptableObject
    {
        [Range(0f, 1f)]
        public float musicVolume;
        [Range(0f, 1f)]
        public float sfxVolume;
        public bool muteMusic;
        public bool muteSFX;
    }
}
