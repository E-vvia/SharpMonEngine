using SharpMonEngine.Unity.Interfaces.Sprites;
using SharpMonEngine.Unity.Model.Sprites;
using UnityEngine;

namespace SharpMonEngine.Unity.Sprites
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(MonoBehaviour))]
    public class OverworldCharacterAnimator : MonoBehaviour
    {
        [field: SerializeField] public OverworldCharacterAnimation UpAnimations { get; set; } = null!;
        [field: SerializeField] public OverworldCharacterAnimation DownAnimations { get; set; } = null!;
        [field: SerializeField] public OverworldCharacterAnimation RightAnimations { get; set; } = null!;
        [field: SerializeField] public OverworldCharacterAnimation LeftAnimations { get; set; } = null!;

        private IOverworldCharacterAnimatable _animator = null!;
        private SpriteRenderer _spriteRenderer = null!;

        private void Awake()
        {
            _animator = GetComponent<IOverworldCharacterAnimatable>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator.OnStepCompleted += OnStepCompleted;
        }

        private void Update()
        {
            OverworldCharacterAnimation? animation = GetAnimation(_animator.Direction);

            animation?.SetSprite(
                _spriteRenderer,
                _animator.Direction,
                _animator.MoveProgress,
                _animator.IsMoving,
                _animator.IsRunning);
        }

        public void OnStepCompleted()
        {
            GetAnimation(_animator.Direction)?.OnStepCompleted();
        }

        private OverworldCharacterAnimation? GetAnimation(Vector2 direction)
        {
            if (direction == Vector2.up)
                return UpAnimations;

            if (direction == Vector2.down)
                return DownAnimations;

            if (direction == Vector2.left)
                return LeftAnimations;

            if (direction == Vector2.right)
                return RightAnimations;

            return null;
        }
    }
}