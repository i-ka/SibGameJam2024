using Code.Scripts.PLayer;
using UnityEngine;
using Zenject;

namespace Code.Scripts.Camera 
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Camera _camera;
        [SerializeField] private float _maxAngleChangePerFrame;
        [SerializeField] private float _minMousePositionChange;

        private PlayerMovementController _playerMovementController;
        private Controls _controls;
        private Vector2 _currentMousePosition = Vector2.zero;
        private Vector2 _previousMousePosition = Vector2.zero;
        private float _directionX = 0;
        private PlayerMovementInfo _playerMovementInfo;

        [Inject]
        public void Construct(PlayerMovementController playerMovementController, Controls controls, 
            PlayerMovementInfo playerMovementInfo)
        {
            _playerMovementController = playerMovementController;
            _controls = controls;
            _playerMovementInfo = playerMovementInfo;
        }

        private void Update()
        {
            transform.position = _playerMovementController.transform.position;
            _previousMousePosition = _currentMousePosition;
            _currentMousePosition = _controls.Camera.MousePosition.ReadValue<Vector2>();

            _directionX = _currentMousePosition.x - _previousMousePosition.x;
            _playerMovementInfo.DirectionAxisZ = transform.forward;
        }

        private void FixedUpdate()
        {
            Debug.Log(_directionX);
            if (Mathf.Abs(_directionX) < _minMousePositionChange || _directionX == 0) return;

            _directionX = _directionX / Mathf.Abs(_directionX);

            var angle = transform.rotation.eulerAngles.y + (_maxAngleChangePerFrame * _directionX);

            var targetRotation = Quaternion.Euler(0, angle, 0);
            transform.rotation = targetRotation;
        }
    }
}