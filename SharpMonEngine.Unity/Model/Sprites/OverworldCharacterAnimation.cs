using System;
using UnityEngine;

namespace SharpMonEngine.Unity.Model.Sprites
{
    [Serializable]
    public class OverworldCharacterAnimation
    {
        [field: SerializeField] public Vector2 Direction { get; set; }

        [field: SerializeField] public Sprite WalkRightStep { get; set; } = null!;
        [field: SerializeField] public Sprite WalkLeftStep { get; set; } = null!;

        [field: SerializeField] public Sprite RunRightStep { get; set; } = null!;
        [field: SerializeField] public Sprite RunLeftStep { get; set; } = null!;

        [field: SerializeField] public Sprite RunIdle { get; set; } = null!;
        [field: SerializeField] public Sprite Idle { get; set; } = null!;
        private bool _stepToggle;

        public bool HasActiveSprite(Vector2 direction)
        {
            return direction == Direction;
        }

        public void SetSprite(
            SpriteRenderer spriteRenderer,
            Vector2 direction,
            float moveProgress,
            bool isMoving,
            bool isRunning)
        {
            if (!HasActiveSprite(direction))
                return;

            if (!isMoving || moveProgress >= 0.5f)
            {
                spriteRenderer.sprite = isRunning ? RunIdle : Idle;
                return;
            }

            spriteRenderer.sprite = GetStepSprite(isRunning);
        }

        public void OnStepCompleted()
        {
            _stepToggle = !_stepToggle;
        }

        private Sprite GetStepSprite(bool isRunning)
        {
            return (isRunning, _stepToggle) switch
            {
                (true, false) => RunRightStep,
                (true, true) => RunLeftStep,
                (false, false) => WalkRightStep,
                (false, true) => WalkLeftStep
            };
        }
    }
}