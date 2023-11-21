using TMPro;
using UnityEngine;

namespace TreasureHunt.Player.UI
{
    public class PercentText : MonoBehaviour
    {
        private TextMeshProUGUI healthPercent;

        private void Awake()
        {
            healthPercent = GetComponent<TextMeshProUGUI>();
        }

        public void SetPercentText(int value)
        {
            healthPercent.SetText(value.ToString() + '%');
        }
    }
}
