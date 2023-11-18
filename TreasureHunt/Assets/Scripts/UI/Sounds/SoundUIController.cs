using UnityEngine;
using UnityEngine.UI;

namespace TreasureHunt.Sounds
{
    public class SoundUIController : MonoBehaviour
    {
        [SerializeField] private Slider musicSlider, sfxSlider;
        [SerializeField] private Toggle musicToggle, sfxToggle;

        private SoundData soundData;
        private SoundManager SMInstance;

        private void Awake()
        {
            SMInstance = SoundManager.Instance;
            soundData = Resources.Load<SoundData>("ScriptableObjects/SoundData");

            SetValuesAtLoad();
        }

        private void OnEnable()
        {
            musicSlider.onValueChanged.AddListener(MusicVolume);
            sfxSlider.onValueChanged.AddListener(SfxVolume);
            musicToggle.onValueChanged.AddListener(ToggleMusic);
            sfxToggle.onValueChanged.AddListener(ToggleSfx);
        }

        private void OnDisable()
        {
            musicSlider.onValueChanged.RemoveAllListeners();
            sfxSlider.onValueChanged.RemoveAllListeners();
            musicToggle.onValueChanged.RemoveAllListeners();
            sfxToggle.onValueChanged.RemoveAllListeners();
        }

        private void SetValuesAtLoad()
        {
            // sound ui values
            musicSlider.value = soundData.musicVolume;
            sfxSlider.value = soundData.sfxVolume;
            musicToggle.isOn = soundData.isMusicOff;
            sfxToggle.isOn = soundData.isSfxOff;

            // sound values
            SMInstance.ToggleMusic(soundData.isMusicOff);
            SMInstance.ToggleSfx(soundData.isSfxOff);
            SMInstance.SetMusicVolume(soundData.musicVolume);
            SMInstance.SetSfxVolume(soundData.sfxVolume);
        }

        private void ToggleMusic(bool value)
        {
            SMInstance.ToggleMusic(value);
            soundData.isMusicOff = value;
        }

        private void ToggleSfx(bool value)
        {
            SMInstance.ToggleSfx(value);
            soundData.isSfxOff = value;
        }

        private void MusicVolume(float value)
        {
            SMInstance.SetMusicVolume(value);
            soundData.musicVolume = value;
        }

        private void SfxVolume(float value)
        {
            SMInstance.SetSfxVolume(value);
            soundData.sfxVolume = value;
        }
    }
}