using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TreasureHunt.InputSystem
{
    [CreateAssetMenu(menuName = "ScriptableObjects/InputReader", fileName = "InputReader")]
    public class InputReader : ScriptableObject, GameInput.IPlayerActions, GameInput.IUIActions
    {
        private GameInput _gameInput;

        public void InitializeGameInput()
        {
            if (_gameInput == null)
            {
                _gameInput = new GameInput();
                _gameInput.Player.SetCallbacks(this);
                _gameInput.UI.SetCallbacks(this);
                SetPlayer();
            }
        }

        // cleanup can be called to destroy or dereference the input reader 
        public void Cleanup()
        {
            if (_gameInput != null)
            {
                _gameInput.Player.Disable();
                _gameInput.UI.Disable();
                _gameInput.Dispose();
                _gameInput = null;
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

        public event Action<Vector2> MoveEvent;
        public event Action<bool, Vector2> LookEvent;
        public event Action<bool> JumpEvent;
        public event Action<bool> SprintEvent;
        public event Action UseEvent;

        public event Action PauseEvent;
        public Action UnpauseAction; // is being called from game manager in one button click
        public Action<string> GameOverAction;
        public Action GameWon;

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                JumpEvent?.Invoke(true);
            }
            if (context.phase == InputActionPhase.Canceled)
            {
                JumpEvent?.Invoke(false);
            }
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            bool isDeviceMouse = context.control.device is Mouse;
            LookEvent?.Invoke(isDeviceMouse, context.ReadValue<Vector2>());
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            MoveEvent?.Invoke(context.ReadValue<Vector2>());
        }

        public void OnSprint(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                SprintEvent?.Invoke(true);
            }
            if (context.phase == InputActionPhase.Canceled)
            {
                SprintEvent?.Invoke(false);
            }
        }

        public void OnUse(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                UseEvent?.Invoke();
            }
        }

        public void OnPause(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                PauseEvent?.Invoke();
            }
        }

        public void OnResume(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                UnpauseAction?.Invoke();
            }
        }
    }
}
