using UnityEngine;
using UnityEngine.UI;

namespace TreasureHunt.Player.UI
{
    public class HealthUI : MonoBehaviour
    {
        private Slider healthSlider;
        private PercentText hpText;

        private void Awake()
        {
            healthSlider = GetComponent<Slider>();
            hpText = GetComponentInChildren<PercentText>();
        }

        private void OnEnable()
        {
            healthSlider.onValueChanged.AddListener(SetPercentValue);
        }

        private void OnDisable()
        {
            healthSlider.onValueChanged.RemoveAllListeners();

        }

        private void SetPercentValue(float sliderValue)
        {
            hpText.SetPercentText((int)sliderValue);

        }

        public void SetHealth(int health)
        {
            healthSlider.value = health;
        }

        public void SetMaxHealth(int maxHealth)
        {
            healthSlider.maxValue = maxHealth;
        }
    }
}
