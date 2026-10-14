using SharpMonEngine.Unity.Controllers;
using SharpMonEngine.Unity.Interfaces.Controllers;
using SharpMonEngine.Unity.Interfaces.Sprites;
using UnityEngine;

namespace SharpMonEngine.Unity.Player
{
    public class PlayerMovement : MonoBehaviour, IOverworldCharacterAnimatable
    {
        [SerializeField] private float walkThreshold = 0.05f;
        [SerializeField] private float runningMultiplier = 1.5f;
        [SerializeField] private float baseSpeed = 2f;

        private float _lerp;
        private IMapController? _mapController;

        private Vector2? _originalPosition;
        private IPlayerController? _playerController;
        private float _speed;
        private Vector2? _targetPosition;

        private float _timeToWalk = 0;

        public void Start()
        {
            _playerController = ControllerContainer.Get<IPlayerController>();
            _mapController = ControllerContainer.Get<IMapController>();
            _speed = baseSpeed;
            transform.position = GetPlayerWorldPosition();
        }

        public void Update()
        {
            if (IsMovementPressed())
            {
                SetDirection();
                IncreateTimeToWalk();

                if (ShouldMove())
                {
                    if (IsRunning() && !_targetPosition.HasValue)
                    {
                        OnPlayerRun();
                    }

                    SetTargetPosition();
                }
            }
            else
            {
                ResetTimeToWalk();
            }

            MoveToTargetPosition();

            if (!_targetPosition.HasValue && !IsRunning())
            {
                ResetSpeed();
            }
        }

        public Vector2 Direction { get; private set; } = Vector2.down;

        public bool Running => !Mathf.Approximately(_speed, baseSpeed);

        private bool IsMovementPressed()
        {
            return _playerController!.Move.IsPressed();
        }

        private void IncreateTimeToWalk()
        {
            _timeToWalk += Time.deltaTime;
            if (_timeToWalk > 1)
            {
                _timeToWalk = 1;
            }
        }

        private bool ShouldMove()
        {
            return _timeToWalk > walkThreshold;
        }

        private bool IsRunning()
        {
            return _playerController!.Run.IsPressed();
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

            _lerp += Time.deltaTime * _speed;

            float t = Mathf.Clamp01(_lerp);

            transform.position = Vector2.Lerp(_originalPosition.Value, _targetPosition.Value, t);

            // ReSharper disable once InvertIf
            // Kept not inverted for clarity
            if (t >= 1f)
            {
                transform.position = GetPlayerWorldPosition();
                _targetPosition = null;
                _originalPosition = null;
                _lerp = 0;
            }
        }

        private void SetDirection()
        {
            Vector2 movement = _playerController!.Move.ReadValue<Vector2>();

            if (movement == Vector2.zero)
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
        }

        private void SetTargetPosition()
        {
            if (_targetPosition.HasValue)
            {
                return;
            }

            Vector3 playerPosition = GetPlayerWorldPosition();
            _originalPosition = playerPosition;
            _targetPosition = _originalPosition + Direction;
        }

        private void ResetTimeToWalk()
        {
            _timeToWalk = 0;
        }

        private Vector3 GetPlayerWorldPosition()
        {
            return _mapController?.GetGridPosition(transform.position) ?? transform.position;
        }

        private void ResetSpeed()
        {
            _speed = baseSpeed;
        }
    }
}