using UnityEngine;

namespace TreasureHunt.Sounds
{
    [System.Serializable]
    public class SoundMusic
    {
        public MusicType name;
        public AudioClip clip;
        [Range(0f, 1f)]
        public float volume;
        [Range(0.1f, 3f)]
        public float pitch;
        public bool isLooping;
    }

    [System.Serializable]
    public class SoundSfx
    {
        public SfxType name;
        public AudioClip clip;
        [Range(0f, 1f)]
        public float volume;
        [Range(0.1f, 3f)]
        public float pitch;
    }
}
