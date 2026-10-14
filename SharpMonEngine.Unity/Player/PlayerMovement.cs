using System;
using SharpMonEngine.Unity.Controllers;
using SharpMonEngine.Unity.Interfaces.Controllers;
using SharpMonEngine.Unity.Interfaces.Sprites;
using UnityEngine;

namespace SharpMonEngine.Unity.Player
{
    public class PlayerMovement : MonoBehaviour, IOverworldCharacterAnimatable
    {
        [Header("Movement")] [SerializeField] private float walkSpeed = 4f;
        [SerializeField] private float runSpeed = 6f;
        [SerializeField] private float inputThreshold = 0.05f;
        private bool _currentStepIsRunning;
        private float _inputHoldTime;

        private IMapController? _mapController;
        private IPlayerController? _playerController;

        private Vector2 _startPosition;
        private float _stepProgress;
        private Vector2 _targetPosition;

        private float CurrentSpeed => _currentStepIsRunning ? runSpeed : walkSpeed;

        private void Start()
        {
            _playerController = ControllerContainer.Get<IPlayerController>();
            _mapController = ControllerContainer.Get<IMapController>();

            transform.position = GetGridWorldPosition();
        }

        private void Update()
        {
            if (_playerController == null || _mapController == null)
                return;

            UpdateMovement();
        }

        public Vector2 Direction { get; private set; } = Vector2.down;
        public Action? OnStepCompleted { get; set; }

        public float MoveProgress => IsMoving ? _stepProgress : 0f;
        public bool IsMoving { get; private set; }

        public bool IsRunning => IsMoving
            ? _currentStepIsRunning
            : _playerController?.Run.IsPressed() == true;

        private void UpdateMovement()
        {
            bool movementPressed = _playerController!.Move.IsPressed();

            if (!movementPressed)
                _inputHoldTime = 0f;
            else
                _inputHoldTime += Time.deltaTime;

            if (IsMoving)
            {
                MoveToTarget();
                return;
            }

            if (!movementPressed || _inputHoldTime < inputThreshold)
                return;

            Vector2 input = _playerController.Move.ReadValue<Vector2>();

            if (input == Vector2.zero)
                return;

            Direction = GetCardinalDirection(input);

            _currentStepIsRunning = _playerController.Run.IsPressed();
            StartStep();
        }

        private void StartStep()
        {
            _startPosition = GetGridWorldPosition();
            _targetPosition = _startPosition + Direction;
            _stepProgress = 0f;
            IsMoving = true;
        }

        private void MoveToTarget()
        {
            _stepProgress += CurrentSpeed * Time.deltaTime;

            float progress = Mathf.Clamp01(_stepProgress);

            transform.position = Vector2.Lerp(
                _startPosition,
                _targetPosition,
                progress);

            if (progress < 1f)
                return;

            transform.position = _targetPosition;
            IsMoving = false;
            _stepProgress = 0f;

            OnStepCompleted?.Invoke();
        }

        private static Vector2 GetCardinalDirection(Vector2 input)
        {
            if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
                return input.x > 0 ? Vector2.right : Vector2.left;

            return input.y > 0 ? Vector2.up : Vector2.down;
        }

        private Vector2 GetGridWorldPosition()
        {
            return _mapController!.GetGridPosition(transform.position);
        }
    }
}