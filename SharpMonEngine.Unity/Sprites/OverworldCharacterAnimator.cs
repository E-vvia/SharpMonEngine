using SharpMonEngine.Unity.Interfaces.Sprites;
using SharpMonEngine.Unity.Model.Sprites;
using UnityEngine;

namespace SharpMonEngine.Unity.Sprites
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class OverworldCharacterAnimator : MonoBehaviour
    {
        [field: SerializeField] public OverworldCharacterAnimation UpAnimations { get; set; } = null!;
        [field: SerializeField] public OverworldCharacterAnimation DownAnimations { get; set; } = null!;
        [field: SerializeField] public OverworldCharacterAnimation RightAnimations { get; set; } = null!;
        [field: SerializeField] public OverworldCharacterAnimation LeftAnimations { get; set; } = null!;

        [SerializeField] private MonoBehaviour animatableMb = null!;
        private SpriteRenderer _spriteRenderer = null!;

        private IOverworldCharacterAnimatable Animatable => (animatableMb as IOverworldCharacterAnimatable)!;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            Animatable.OnStepCompleted += OnStepCompleted;
        }

        private void Update()
        {
            OverworldCharacterAnimation? oveworldAnimation = GetAnimation(Animatable.Direction);

            oveworldAnimation?.SetSprite(
                _spriteRenderer,
                Animatable.Direction,
                Animatable.MoveProgress,
                Animatable.IsMoving,
                Animatable.IsRunning);
        }

        public void OnStepCompleted()
        {
            GetAnimation(Animatable.Direction)?.OnStepCompleted();
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