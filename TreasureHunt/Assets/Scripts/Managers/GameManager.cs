using TreasureHunt.InputSystem;
using UnityEngine;

namespace TreasureHunt
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private PauseMenuPanel pauseMenu;
        [SerializeField] private GameOverPanel gameOverMenu;
        [SerializeField] private GameOverPanel gameWonMenu;

        private InputReader _input;
        public InputReader InputReader { get { return _input; } }

        private void Awake()
        {
            _input = Resources.Load<InputReader>("InputSystem/InputReader");
        }

        private void OnEnable()
        {
            SubscribePauseEvents();
            _input.GameOverAction += _input_GameOverAction;
            _input.GameWon += GameWin;
        }

        private void OnDisable()
        {
            UnsubscribePauseEvents();
            _input.GameOverAction -= _input_GameOverAction;
            _input.GameWon -= GameWin;
        }

        private void Start()
        {
            pauseMenu.SetGameManager(this);
        }

        private void SubscribePauseEvents()
        {
            _input.PauseEvent += _input_PauseEvent;
            _input.UnpauseEvent += _input_UnpauseEvent;
        }

        private void UnsubscribePauseEvents()
        {
            _input.PauseEvent -= _input_PauseEvent;
            _input.UnpauseEvent -= _input_UnpauseEvent;
        }

        private void _input_UnpauseEvent()
        {
            _input.SetPlayer();
            Debug.Log("Player: " + _input._gameInput.Player.enabled + ", UI: " + _input._gameInput.UI.enabled);
            if (pauseMenu.gameObject.activeSelf)
            {
                pauseMenu.gameObject.SetActive(false);
            }
        }

        private void _input_PauseEvent()
        {
            _input.SetUI();
            Debug.Log("Player: " + _input._gameInput.Player.enabled + ", UI: " + _input._gameInput.UI.enabled);
            if (!pauseMenu.gameObject.activeSelf)
            {
                pauseMenu.gameObject.SetActive(true);
            }
        }

        private void _input_GameOverAction(string reason)
        {
            gameOverMenu.gameObject.SetActive(true);
            gameOverMenu.SetReasonText(reason);
            UnsubscribePauseEvents();
        }

        private void GameWin()
        {
            gameWonMenu.gameObject.SetActive(true);
            UnsubscribePauseEvents();
        }

        // return to game from pause menu, via button click
        public void ReturnToGame()
        {
            _input.UnpauseEvent?.Invoke();
        }
    }
}
