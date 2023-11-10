using System;
using UnityEngine;

namespace TreasureHunt.Sounds
{
    public class SoundManager : GenericMonoSingleton<SoundManager>
    {
        [SerializeField] private SoundMusic[] musicSounds;
        [SerializeField] private SoundSfx[] sfxSounds;
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource sfxSource;


        protected override void Awake()
        {
            base.Awake();
            PlayMusic(MusicType.Bg_Seaside_Waves);
        }

        private void PlayAudioMusic(SoundMusic sound, bool isLoop)
        {
            musicSource.clip = sound.clip;
            //musicSource.volume = sound.volume;
            musicSource.pitch = sound.pitch;
            musicSource.loop = isLoop;
            musicSource.Play();
        }

        private void PlayAudioSfx(SoundSfx sound)
        {
            //sfxSource.volume = sound.volume;
            sfxSource.pitch = sound.pitch;
            sfxSource.PlayOneShot(sound.clip);
        }


        public void PlayMusic(MusicType sName)
        {
            SoundMusic s = Array.Find(musicSounds, item => item.name == sName);
            if (s == null)
            {
                Debug.LogWarning("Sound file: " + sName + " was not Found.");
                return;
            }
            PlayAudioMusic(s, s.isLooping);
        }

        public void PlaySfx(SfxType sName)
        {
            SoundSfx sound = Array.Find(sfxSounds, item => item.name == sName);
            if (sound == null)
            {
                Debug.LogWarning("Sound sfx: " + sName + " was not Found.");
                return;
            }
            PlayAudioSfx(sound);
        }

        public void ToggleMusic(bool isMute)
        {
            musicSource.mute = isMute;
        }

        public void ToggleSfx(bool isMute)
        {
            sfxSource.mute = isMute;
        }

        public void SetMusicVolume(float value)
        {
            musicSource.volume = value;
        }

        public void SetSfxVolume(float value)
        {
            sfxSource.volume = value;
        }
    }
}