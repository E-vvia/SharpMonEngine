using System;
using SharpMonEngine.Unity.Interfaces.Controllers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SharpMonEngine.Unity.Controllers
{
    [DefaultExecutionOrder(-99)]
    public class PlayerController : MonoBehaviour, IPlayerController
    {
        private InputAction _cancelAction = null!;
        private InputAction _confirmAction = null!;
        private InputAction _moveAction = null!;
        private InputAction _onRunAction = null!;

        public void Awake()
        {
            ControllerContainer.Register<IPlayerController, PlayerController>(this);
        }

        public void Start()
        {
            _moveAction = InputSystem.actions.FindAction("Move");
            _confirmAction = InputSystem.actions.FindAction("Confirm");
            _cancelAction = InputSystem.actions.FindAction("Cancel");
            _onRunAction = InputSystem.actions.FindAction("Run");
        }

        public void Update()
        {
            Vector2 moveValue = _moveAction.ReadValue<Vector2>();
            bool confirm = _confirmAction.WasPressedThisFrame();
            bool cancel = _cancelAction.WasPressedThisFrame();
            bool run = _onRunAction.IsPressed();

            OnMove?.Invoke(moveValue);

            if (confirm)
            {
                OnConfirmPressed?.Invoke();
            }

            if (cancel)
            {
                OnCancelPressed?.Invoke();
            }

            if (run)
            {
                Debug.Log("Run!");
                OnRun?.Invoke();
            }
        }

        public void OnDestroy()
        {
        }

        public event Action<Vector2>? OnMove;
        public event Action? OnConfirmPressed;
        public event Action? OnCancelPressed;
        public event Action? OnRun;
    }
}