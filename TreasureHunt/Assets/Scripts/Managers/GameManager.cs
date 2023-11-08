using TreasureHunt.InputSystem;
using UnityEngine;

namespace TreasureHunt
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject pauseMenu;

        private InputReader _input;
        public InputReader InputReader { get { return _input; } }

        private void Awake()
        {
            _input = Resources.Load<InputReader>("InputSystem/InputReader");
        }

        private void OnEnable()
        {
            _input.PauseEvent += _input_PauseEvent;
            _input.UnpauseEvent += _input_UnpauseEvent;
        }

        private void OnDisable()
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
    }
}
