using TMPro;
using TreasureHunt.MainMenu;
using UnityEngine;
using UnityEngine.UI;

namespace TreasureHunt
{
    public class GameOverPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI reasonText;
        [SerializeField] private Button menuBtn;
        [SerializeField] private Button restartBtn;

        private LevelManager levelManager;

        private void Awake()
        {
            levelManager = new LevelManager();
        }

        private void OnEnable()
        {
            menuBtn.onClick.AddListener(LoadMenu);
            restartBtn.onClick.AddListener(RestartLevel);
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
            restartBtn.onClick.RemoveAllListeners();
        }
    }
}
