using UnityEngine;
using UnityEngine.UI;

namespace TreasureHunt.Player.UI
{
    [RequireComponent(typeof(Slider))]
    public class HealthUI : MonoBehaviour
    {
        [SerializeField] private Slider healthSlider;
        [SerializeField] private PercentText hpText;

        private void SetPercentValue(float sliderValue)
        {
            hpText.SetPercentText((int)sliderValue);

        }

        public void SetHealth(int health)
        {
            healthSlider.value = health;
            SetPercentValue(health);
        }

        public void SetMaxHealth(int maxHealth)
        {
            healthSlider.maxValue = maxHealth;
        }
    }
}
