using UnityEngine;
using UnityEngine.UI;

namespace TreasureHunt.Assets.Scripts.UI.Sounds
{
    public class SoundUIController : MonoBehaviour
    {
        [SerializeField] private Slider musicSlider, sfxSlider;


        public void ToggleMusic()
        {
            SoundManager.Instance.ToggleMusic();
        }


        public void ToggleSfx()
        {
            SoundManager.Instance.ToggleSfx();
        }

        public void MusicVolume()
        {
            SoundManager.Instance.SetMusicVolume(musicSlider.value);
        }

        public void SfxVolume()
        {
            SoundManager.Instance.SetSfxVolume(musicSlider.value);
        }
    }
}