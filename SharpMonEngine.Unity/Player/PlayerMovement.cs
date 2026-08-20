using SharpMonEngine.Unity.Controllers;
using SharpMonEngine.Unity.Interfaces.Controllers;
using UnityEngine;

namespace SharpMonEngine.Unity.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float runningMultiplier = 2f;
        [SerializeField] private float baseSpeed = 5;
        private Vector2 _direction = Vector2.down;
        private float _lerp = 0;
        private IMapController? _mapController;
        private Vector2? _originalPosition;
        private IPlayerController? _playerController;
        private float _speed = 0;
        private Vector2? _targetPosition;

        public void Start()
        {
            _playerController = ControllerContainer.Get<IPlayerController>();
            _mapController = ControllerContainer.Get<IMapController>();
            _playerController.OnMove += OnPlayerMove;
            _playerController.OnRun += OnPlayerRun;
            _speed = baseSpeed;
            transform.position = GetPlayerWorldPosition();
        }

        void Update()
        {
            MoveToTargetPosition();
            _speed = baseSpeed;
        }

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
            //We do it by _lerp * because:
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

        private void OnPlayerMove(Vector2 movement)
        {
            if (_targetPosition != null || _originalPosition != null || movement == Vector2.zero)
            {
                return;
            }

            if (movement.x > 0)
            {
                _direction = Vector2.right;
            }
            else if (movement.x < 0)
            {
                _direction = Vector2.left;
            }
            else if (movement.y > 0)
            {
                _direction = Vector2.up;
            }
            else if (movement.y < 0)
            {
                _direction = Vector2.down;
            }

            Vector3 playerPosition = GetPlayerWorldPosition();
            _originalPosition = playerPosition;
            _targetPosition = _originalPosition + _direction;
        }

        private Vector3 GetPlayerWorldPosition()
        {
            return _mapController?.GetGridPosition(transform.position) ?? transform.position;
        }
    }
}