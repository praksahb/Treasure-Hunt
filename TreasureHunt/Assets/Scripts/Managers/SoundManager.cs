using System;
using TreasureHunt.Sounds;
using UnityEngine;

namespace TreasureHunt
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
            // continue with any awake functionality
            // InitializeAudioSource();
        }

        private void InitializeAudioSource()
        {
            //foreach (Sound s in sounds)
            //{
            //    s.source = gameObject.AddComponent<AudioSource>();
            //    s.source.clip = s.clip;
            //    s.source.volume = s.volume;
            //    s.source.pitch = s.pitch;
            //}

        }

        private void PlayAudioMusic(SoundMusic sound)
        {
            musicSource.clip = sound.clip;
            musicSource.volume = sound.volume;
            musicSource.pitch = sound.pitch;
            musicSource.Play();
        }

        private void PlayAudioSfx(SoundSfx sound)
        {
            sfxSource.volume = sound.volume;
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
            PlayAudioMusic(s);
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

        public void ToggleMusic()
        {
            musicSource.mute = !musicSource.mute;
        }

        public void ToggleSfx()
        {
            sfxSource.mute = !sfxSource.mute;
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