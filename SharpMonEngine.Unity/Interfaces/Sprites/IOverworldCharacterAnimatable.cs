using UnityEngine;

namespace SharpMonEngine.Unity.Interfaces.Sprites
{
    public interface IOverworldCharacterAnimatable
    {
        Vector2 Direction { get; }
        bool Running { get; }
    }
}