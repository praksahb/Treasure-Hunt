using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TreasureHunt.InputSystem
{
    [CreateAssetMenu(menuName = "ScriptableObjects/InputReader", fileName = "InputReader")]
    public class InputReader : ScriptableObject, GameInput.IPlayerActions, GameInput.IUIActions
    {
        private GameInput _gameInput;

        private void OnEnable()
        {
            if (_gameInput == null)
            {
                _gameInput = new GameInput();
                _gameInput.Player.SetCallbacks(this);
                _gameInput.UI.SetCallbacks(this);
                SetPlayer();
            }
        }

        public void SetPlayer()
        {
            _gameInput.Player.Enable();
            _gameInput.UI.Disable();
        }

        public void SetUI()
        {
            _gameInput.Player.Disable();
            _gameInput.UI.Enable();
        }

        // currently only using pause and unpause 
        // rest all are being managed by the standard assets using the unity messages

        public event Action PauseEvent;
        public event Action UnpauseEvent;
        public Action<bool> GameOverAction;

        private void GameOverSwitchDefaultMap(bool value)
        {
            if (!value)
            {
                SetPlayer();
            }
            else
            {
                SetUI();
            }
        }

        public void OnJump(InputAction.CallbackContext context)
        {

        }

        public void OnLook(InputAction.CallbackContext context)
        {

        }

        public void OnMove(InputAction.CallbackContext context)
        {
            Debug.Log("val: " + context.ReadValue<Vector2>());
        }

        public void OnSprint(InputAction.CallbackContext context)
        {

        }

        public void OnUse(InputAction.CallbackContext context)
        {
            Debug.Log("USE: " + context.phase);
        }

        public void OnPause(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                PauseEvent?.Invoke();
                SetUI();
            }
        }

        public void OnResume(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                UnpauseEvent?.Invoke();
                SetPlayer();
            }
        }
    }
}
