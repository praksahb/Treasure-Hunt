using TreasureHunt.InputSystem;
using UnityEngine;

namespace TreasureHunt
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject pauseMenu;
        [SerializeField] private GameObject gameOverMenu = null;

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
        }


        private void OnDisable()
        {
            UnsubscribePauseEvents();
            _input.GameOverAction -= _input_GameOverAction;
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
            pauseMenu.SetActive(false);
        }

        private void _input_PauseEvent()
        {
            pauseMenu.SetActive(true);
        }

        private void _input_GameOverAction(bool value)
        {
            if (!value)
            {
                gameOverMenu.SetActive(false);
            }
            else
            {
                gameOverMenu.SetActive(true);
                UnsubscribePauseEvents();
            }
        }
    }
}
