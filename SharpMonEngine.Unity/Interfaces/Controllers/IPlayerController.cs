using System;
using UnityEngine;

namespace SharpMonEngine.Unity.Interfaces.Controllers
{
    public interface IPlayerController
    {
        event Action<Vector2>? OnMove;
        event Action? OnConfirmPressed;
        event Action? OnCancelPressed;
        event Action? OnRun;
    }
}