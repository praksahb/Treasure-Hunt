using System;
using UnityEngine;

namespace TreasureHunt.Sounds
{
    public class SoundManager : GenericMonoSingleton<SoundManager>
    {
        [SerializeField] private SoundMusic[] musicSounds;
        [SerializeField] private SoundSfx[] sfxSounds;
        [SerializeField] private AudioSource musicSource;

        private float sfxVolume;
        private bool sfxMute;

        protected override void Awake()
        {
            base.Awake();
            PlayMusic(MusicType.Bg_Seaside_Waves);
        }

        private void PlayAudioMusic(SoundMusic sound, bool isLoop)
        {
            musicSource.clip = sound.clip;
            musicSource.pitch = sound.pitch;
            musicSource.loop = isLoop;
            musicSource.Play();
        }

        private void PlayAudioSfx(SoundSfx sound, AudioSource sfxSource)
        {
            sfxSource.volume = sfxVolume;
            sfxSource.mute = sfxMute;
            sfxSource.pitch = sound.pitch;
            sfxSource.PlayOneShot(sound.clip);
        }

        private AudioClip TrimSoundClip(AudioClip originalClip, float targetDuration)
        {
            // Get the original duration of the sound clip
            float originalDuration = originalClip.length;

            // Calculate the target sample count based on the target duration
            int targetSampleCount = (int)(originalClip.samples * targetDuration / originalDuration);

            // Create a new array to hold the trimmed samples
            float[] samples = new float[targetSampleCount];

            // Copy the samples from the original clip into the new array
            originalClip.GetData(samples, 0);

            // Create a new AudioClip with the trimmed samples
            AudioClip newClip = AudioClip.Create(originalClip.name, targetSampleCount, 1, originalClip.frequency, false);
            newClip.SetData(samples, 0);

            // Return the trimmed AudioClip
            return newClip;
        }

        public void UpdateSoundSfx(SfxType doorClosed, float doorCloseClipLength)
        {
            // Trim the sound clip in the SoundManager
            SoundSfx sound = Array.Find(sfxSounds, item => item.name == doorClosed);
            if (sound == null)
            {
                Debug.LogWarning("Sound sfx: " + doorClosed + " was not Found.");
                return;
            }
            sound.clip = TrimSoundClip(sound.clip, doorCloseClipLength);
        }


        // Properties
        public float CurrentMusicVolume => musicSource.volume;
        //public float CurrentSFXVolume => sfxSource.volume;
        public bool IsMusicOn => musicSource.mute;
        //public bool IsSFXOn => sfxSource.mute;

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

        public void PlaySfx(SfxType sName, AudioSource audioSource)
        {
            if (audioSource == null)
            {
                Debug.Log("Audio Source not found.");
            }
            SoundSfx sound = Array.Find(sfxSounds, item => item.name == sName);
            if (sound == null)
            {
                Debug.LogWarning("Sound sfx: " + sName + " was not Found.");
                return;
            }
            //Debug.Log("name:" + sound.name + ", len: " + sound.clip.length);
            PlayAudioSfx(sound, audioSource);
        }

        public void ToggleMusic(bool isMute)
        {
            musicSource.mute = isMute;
        }

        public void ToggleSfx(bool isMute)
        {
            sfxMute = isMute;
        }

        public void SetMusicVolume(float value)
        {
            musicSource.volume = value;
        }

        public void SetSfxVolume(float value)
        {
            sfxVolume = value;
        }
    }
}