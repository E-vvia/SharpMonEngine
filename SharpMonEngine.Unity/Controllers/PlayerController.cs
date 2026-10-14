using SharpMonEngine.Unity.Interfaces.Controllers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SharpMonEngine.Unity.Controllers
{
    [DefaultExecutionOrder(-99)]
    public class PlayerController : MonoBehaviour, IPlayerController
    {
        public void Awake()
        {
            ControllerContainer.Register<IPlayerController, PlayerController>(this);
        }

        public void Start()
        {
            Move = InputSystem.actions.FindAction("Move");
            Confirm = InputSystem.actions.FindAction("Confirm");
            Cancel = InputSystem.actions.FindAction("Cancel");
            Run = InputSystem.actions.FindAction("Run");
        }

        public void OnDestroy()
        {
            ControllerContainer.UnRegister<IPlayerController>();
        }

        public InputAction Cancel { get; private set; } = null!;

        public InputAction Confirm { get; private set; } = null!;

        public InputAction Move { get; private set; } = null!;

        public InputAction Run { get; private set; } = null!;
    }
}