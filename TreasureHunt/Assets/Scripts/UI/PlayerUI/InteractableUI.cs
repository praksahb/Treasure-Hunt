using TMPro;
using TreasureHunt.Interactions;
using UnityEngine;

namespace TreasureHunt.Player.UI
{
    public class InteractableUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI headerField;
        [SerializeField] private TextMeshProUGUI textField;

        private InteractionData interactionData;

        private void Awake()
        {
            interactionData = Resources.Load<InteractionData>("ScriptableObjects/InteractionData");
        }

        private void SetText(string header, string text)
        {
            headerField.SetText(header);
            textField.SetText(text);
        }

        public void SetupInteractionUI(InteractionType interactionType)
        {
            int index = (int)interactionType;
            InteractableData interaction = interactionData.interactables[index];
            SetText(interaction.headerField, interaction.textField);
        }
    }
}
