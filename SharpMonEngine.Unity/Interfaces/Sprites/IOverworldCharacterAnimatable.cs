using System;
using UnityEngine;

namespace SharpMonEngine.Unity.Interfaces.Sprites
{
    public interface IOverworldCharacterAnimatable
    {
        Action? OnStepCompleted { get; set; }
        float MoveProgress { get; }
        Vector2 Direction { get; }
        bool IsMoving { get; }
        bool IsRunning { get; }
    }
}