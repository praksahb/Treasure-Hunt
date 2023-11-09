using TMPro;
using TreasureHunt.Interactions;
using UnityEngine;

namespace TreasureHunt
{
    public class CollectedKey : MonoBehaviour
    {
        [SerializeField] private KeyType keyType;
        [SerializeField] private TextMeshProUGUI collectedText;

        public KeyType KeyType { get { return keyType; } }

        public void KeyCollected()
        {
            collectedText.SetText("Collected");
        }
    }
}
