using TMPro;
using UnityEngine;

namespace TreasureHunt.Player.UI
{
    public class InteractableUI : MonoBehaviour
    {
        private TextMeshProUGUI textField;


        private void Awake()
        {
            textField = GetComponent<TextMeshProUGUI>();
        }


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
