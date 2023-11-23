using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TreasureHunt.UI
{
    // also works as game won panel
    public class GameOverPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI reasonText = null;
        [SerializeField] private Button menuBtn;
        [SerializeField] private Button restartBtn = null;

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
            LevelLoader.Instance.LoadLevel(Level.MainMenu);
        }

        private void RestartLevel()
        {
            LevelLoader.Instance.LoadLevel(Level.Restart);
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
