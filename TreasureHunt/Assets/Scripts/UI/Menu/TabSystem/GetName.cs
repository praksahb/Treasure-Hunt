using TMPro;
using UnityEngine;

namespace TreasureHunt
{
    public class GetName : MonoBehaviour
    {
        [SerializeField] private GameObject parent;

        private TextMeshProUGUI text;

        private void Awake()
        {
            text = GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            text.SetText(parent.name);
        }

        private void OnGUI()
        {
            text.SetText(parent.name);
        }
    }
}
