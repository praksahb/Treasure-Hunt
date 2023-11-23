using TMPro;
using TreasureHunt.MainMenu;
using UnityEngine;
using UnityEngine.UI;

namespace TreasureHunt
{
    // also works as game won panel
    public class GameOverPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI reasonText = null;
        [SerializeField] private Button menuBtn;
        [SerializeField] private Button restartBtn = null;

        private LevelManager levelManager;

        private void Awake()
        {
            levelManager = new LevelManager();
        }

        private void OnEnable()
        {
            menuBtn.onClick.AddListener(LoadMenu);
            if (restartBtn != null)
            {
                restartBtn?.onClick.AddListener(RestartLevel);
            }
        }

        private void LoadMenu()
        {
            levelManager.LoadMainMenu();
        }

        private void RestartLevel()
        {
            levelManager.RestartLevel();
        }

        private void OnDisable()
        {
            menuBtn.onClick.RemoveAllListeners();
            if (restartBtn != null)
            {
                restartBtn.onClick.RemoveAllListeners();
            }
        }

        public void SetReasonText(string reason)
        {
            if (reasonText != null)
            {
                reasonText.SetText(reason);
            }
        }
    }
}
