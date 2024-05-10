using Code.Scripts.PLayer;
using UnityEngine;
using Zenject;

namespace Code.Scripts.Camera 
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Camera _camera;
        [SerializeField] private Transform _transformForForwardDirection;
        [SerializeField] private float _maxAngleChangePerFrame;
        [SerializeField] private Vector2 _minMousePositionChange;
        [SerializeField] private int _maxRotationAxisY;

        private PlayerMovementController _playerMovementController;
        private Controls _controls;
        private Vector2 _currentMousePosition = Vector2.zero;
        private Vector2 _previousMousePosition = Vector2.zero;
        private float _directionX = 0;
        private float _directionY = 0;
        private PlayerMovementInfo _playerMovementInfo;

        public const int SemiCircleDegrees = 180;
        public const int FullCircleDegrees = 360;

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
            _directionY = _currentMousePosition.y - _previousMousePosition.y;

            _playerMovementInfo.DirectionAxisZ = _transformForForwardDirection.forward;
        }

        private void FixedUpdate()
        {
            var abgleAxisY = GetAngle(ref _directionX, _minMousePositionChange.x, transform.rotation.eulerAngles.y);
            var angleAxisX = (int)GetAngle(ref _directionY, _minMousePositionChange.y, 0);
       
            var targetRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, abgleAxisY, 0);

            var actualAngle = (int)transform.rotation.eulerAngles.x;
            if (DegreesCondition()) 
            {
                actualAngle = (int)(transform.rotation.eulerAngles.x - FullCircleDegrees);
            }

            if(RotationCondition(actualAngle))
            {
                targetRotation *= Quaternion.Euler(angleAxisX, 0, 0);
            }

            transform.rotation = targetRotation;
        }

        private bool DegreesCondition() 
        {
            return transform.rotation.eulerAngles.x > SemiCircleDegrees;
        }

        private bool RotationCondition(int actualAngle) 
        {
            return (_directionY < 0 && actualAngle >= -_maxRotationAxisY) || 
                (_directionY > 0 && actualAngle <= _maxRotationAxisY);
        }

        private float GetAngle(ref float direction, float minChange, float eulerAngle) 
        {
            if (Mathf.Abs(direction) < minChange || direction == 0) return eulerAngle;

            direction = direction / Mathf.Abs(direction);

            return eulerAngle + (_maxAngleChangePerFrame * direction);
        }
    }
}