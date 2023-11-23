using UnityEngine;
using UnityEngine.UI;

namespace TreasureHunt.UI
{
    public class MainMenuPanel : MonoBehaviour
    {
        [SerializeField] private Button startGameBtn;

        private void OnEnable()
        {
            startGameBtn.onClick.AddListener(LoadTestLevel);
        }

        private void OnDisable()
        {
            startGameBtn.onClick.RemoveAllListeners();
        }

        private void LoadTestLevel()
        {
            LevelLoader.Instance.LoadLevel(Level.TestLevel);
        }
    }
}
