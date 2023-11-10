using UnityEngine;
using UnityEngine.UI;

namespace TreasureHunt.Sounds
{
    public class SoundUIController : MonoBehaviour
    {
        [SerializeField] private Slider musicSlider, sfxSlider;
        [SerializeField] private Toggle musicToggle, sfxToggle;

        private void OnEnable()
        {
            musicSlider.onValueChanged.AddListener(MusicVolume);
            sfxSlider.onValueChanged.AddListener(SfxVolume);

            musicToggle.onValueChanged.AddListener(ToggleMusic);
            sfxToggle.onValueChanged.AddListener(ToggleSfx);
        }

        private void Start()
        {
            MusicVolume(musicSlider.value);
            SfxVolume(sfxSlider.value);
        }

        private void OnDisable()
        {
            musicSlider.onValueChanged.RemoveAllListeners();
            sfxSlider.onValueChanged.RemoveAllListeners();
            musicToggle.onValueChanged.RemoveAllListeners();
            sfxToggle.onValueChanged.RemoveAllListeners();
        }

        private void ToggleMusic(bool value)
        {
            SoundManager.Instance.ToggleMusic(value);
        }

        private void ToggleSfx(bool value)
        {
            SoundManager.Instance.ToggleSfx(value);
        }

        private void MusicVolume(float value)
        {
            SoundManager.Instance.SetMusicVolume(value);
        }

        private void SfxVolume(float value)
        {
            SoundManager.Instance.SetSfxVolume(value);
        }
    }
}