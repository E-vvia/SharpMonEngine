using SharpMonEngine.Unity.Controllers;
using SharpMonEngine.Unity.Interfaces.Controllers;
using SharpMonEngine.Unity.Interfaces.Sprites;
using UnityEngine;

namespace SharpMonEngine.Unity.Player
{
    public class PlayerMovement : MonoBehaviour, IOverworldCharacterAnimatable
    {
        [SerializeField] private float runningMultiplier = 2f;
        [SerializeField] private float baseSpeed = 5;

        public void Start()
        {
            _playerController = ControllerContainer.Get<IPlayerController>();
            _mapController = ControllerContainer.Get<IMapController>();
            _speed = baseSpeed;
            transform.position = GetPlayerWorldPosition();
        }

        public void Update()
        {
            SetTargetPosition();
            MoveToTargetPosition();

            if (_targetPosition == null)
            {
                ResetSpeed();
            }
        }

        public Vector2 Direction { get; private set; } = Vector2.down;

        public bool Running => !Mathf.Approximately(_speed, baseSpeed);

        private void OnPlayerRun()
        {
            _speed = baseSpeed * runningMultiplier;
        }

        private void MoveToTargetPosition()
        {
            if (!_targetPosition.HasValue || !_originalPosition.HasValue)
            {
                return;
            }

            //We wanna interpolate betwen Point A (origin cell) to Point B (destination cell)
            //We do it by _lerp * _speed because:
            //1. It doesn't slow down when reaching Point B.
            //2. We can make it faster or slower.
            float t = _lerp * _speed;
            transform.position = Vector2.Lerp(_originalPosition.Value, _targetPosition.Value, t);

            _lerp += Time.deltaTime;

            // ReSharper disable once InvertIf
            // Kept not inverted for clarity
            if (Vector2.Distance(transform.position, _targetPosition.Value) < 0.01f)
            {
                transform.position = GetPlayerWorldPosition();
                _targetPosition = null;
                _originalPosition = null;
                _lerp = 0;
            }
        }

        private void SetTargetPosition()
        {
            Vector2 movement = _playerController!.Move.ReadValue<Vector2>();
            bool movementPressed = _playerController.Move.WasPressedThisFrame();

            if (movement == Vector2.zero || _targetPosition.HasValue)
            {
                return;
            }

            if (movement.x > 0)
            {
                Direction = Vector2.right;
            }

            if (movement.x < 0)
            {
                Direction = Vector2.left;
            }

            if (movement.y > 0)
            {
                Direction = Vector2.up;
            }

            if (movement.y < 0)
            {
                Direction = Vector2.down;
            }

            Vector3 playerPosition = GetPlayerWorldPosition();

            // ReSharper disable once InvertIf
            // Kept not inverted for clarity
            if (!movementPressed)
            {
                _originalPosition = playerPosition;
                _targetPosition = _originalPosition + Direction;
            }
        }

        private Vector3 GetPlayerWorldPosition()
        {
            return _mapController?.GetGridPosition(transform.position) ?? transform.position;
        }

        private void ResetSpeed()
        {
            _speed = baseSpeed;
        }

        #region Controllers

        private IMapController? _mapController;
        private IPlayerController? _playerController;

        #endregion

        #region PositionalVectors

        private Vector2? _originalPosition;
        private Vector2? _targetPosition;

        #endregion

        #region Lerp and speed variables

        private float _lerp;
        private float _speed;

        private bool _movementPressed = false;

        #endregion
    }
}