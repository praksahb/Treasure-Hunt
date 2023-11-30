using UnityEngine;

namespace TreasureHunt.Interactions
{
    [CreateAssetMenu(fileName = "InteractionsData", menuName = "ScriptableObjects/InteractionsData")]
    public class InteractionData : ScriptableObject
    {
        [Tooltip("Change text to be displayed in UI when interacting with interactables from here")]
        public InteractableData[] interactables;
    }

    [System.Serializable]
    public class InteractableData
    {
        public InteractionType interactionType;
        [TextAreaAttribute]
        public string headerField;
        [TextAreaAttribute]
        public string textField;
    }
}