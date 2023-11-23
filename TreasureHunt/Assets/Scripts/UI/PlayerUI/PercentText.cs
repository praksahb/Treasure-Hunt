using TMPro;
using UnityEngine;

namespace TreasureHunt.Player.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class PercentText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI healthPercent;

        public void SetPercentText(int value)
        {
            healthPercent.SetText(value.ToString() + '%');
        }
    }
}
