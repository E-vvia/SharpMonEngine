using UnityEngine.InputSystem;

namespace SharpMonEngine.Unity.Interfaces.Controllers
{
    public interface IPlayerController
    {
        InputAction Cancel { get; }
        InputAction Confirm { get; }
        InputAction Move { get; }
        InputAction Run { get; }
    }
}