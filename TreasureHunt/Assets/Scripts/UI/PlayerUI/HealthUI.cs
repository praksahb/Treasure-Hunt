using UnityEngine;
using UnityEngine.UI;

namespace TreasureHunt.Player.UI
{
    public class HealthUI : MonoBehaviour
    {
        private Slider healthSlider;

        private void Awake()
        {
            healthSlider = GetComponent<Slider>();
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
