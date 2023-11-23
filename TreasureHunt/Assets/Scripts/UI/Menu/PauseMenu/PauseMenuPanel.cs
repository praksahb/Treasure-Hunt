using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TreasureHunt.UI
{
    public class PauseMenuPanel : MonoBehaviour
    {
        [SerializeField] private Button menuButton;
        [SerializeField] private Button returnButton;

        private GameManager gameManager;
        private LevelLoader levelLoaderInstance;

        private void Awake()
        {
            levelLoaderInstance = LevelLoader.Instance;
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
            levelLoaderInstance.LoadLevel(Level.MainMenu);
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
