using TMPro;
using UnityEngine;

namespace TreasureHunt.Player.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class InteractableUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textField;

        public void SetText(string text)
        {
            textField.SetText(text);
        }

        public void ClearText()
        {
            textField.SetText("");
        }
    }
}
