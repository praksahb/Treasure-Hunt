using TreasureHunt.MainMenu;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TreasureHunt
{
    public class PauseMenuPanel : MonoBehaviour
    {
        [SerializeField] private Button menuButton;
        [SerializeField] private Button returnButton;

        private LevelManager levelManager;
        private GameManager gameManager;

        private void Awake()
        {
            levelManager = new LevelManager();
        }

        private void OnEnable()
        {
            menuButton.onClick.AddListener(GoToMainMenuScene);
            returnButton.onClick.AddListener(ReturnToGame);

            // reset button selected color
            EventSystem.current.SetSelectedGameObject(null);
        }

        private void OnDisable()
        {
            menuButton.onClick.RemoveAllListeners();
            returnButton.onClick.RemoveAllListeners();
        }

        private void GoToMainMenuScene()
        {
            levelManager.LoadMainMenu();
        }

        private void ReturnToGame()
        {
            gameManager.ReturnToGame();
        }

        public void SetGameManager(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }
    }
}
