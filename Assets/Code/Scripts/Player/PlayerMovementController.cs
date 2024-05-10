using UnityEngine;
using Zenject;

namespace Code.Scripts.PLayer 
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;

        [Header("Movement")]
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _velocitySpeed;

        [Header("Rotation")]
        [SerializeField] private float _ratationSpeed;
        [SerializeField] private float _ratationForwardVectorSpeed;
        [SerializeField] private Transform _rotationTransform;

        [Header("Jumping")]
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private float _jumpRaycstDistance;
        [SerializeField] private float _jumpVelocity;
        [SerializeField] private Transform _raycastTransform;

        private Controls _controls;
        private Vector3 _targetVelocity = Vector3.zero;
        private Vector3 _direction = Vector3.zero;
        private Vector3 _localDirection = Vector3.zero;
        private Quaternion _targetRotation = Quaternion.identity;

        private bool _isOnTheGround = false;
        private PlayerMovementInfo _playerMovementInfo;

        public float MovementSpeed => _movementSpeed;
        public Rigidbody Rigidbody => _rigidbody;

        [Inject]
        public void Construct(Controls controls, PlayerMovementInfo playerMovementInfo) 
        {
            _controls = controls;
            _playerMovementInfo = playerMovementInfo;
        }      

        private void Update()
        {
            if (_controls == null) return;

            CalculateVelocity();
            CalculateJumpVelocity();
            CalculateRotation();
        }

        private void FixedUpdate()
        {
            if (_controls == null) return;

            SetTransformForwardVector();

            var currenVelocity = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);
            currenVelocity = Vector3.MoveTowards(currenVelocity, _targetVelocity, _velocitySpeed * Time.fixedDeltaTime);
            _rigidbody.velocity = new Vector3(currenVelocity.x, _rigidbody.velocity.y, currenVelocity.z);

            _rotationTransform.localRotation = Quaternion.RotateTowards(_rotationTransform.localRotation,
                _targetRotation, Time.fixedDeltaTime * _ratationSpeed);
        }     

        private void CalculateVelocity() 
        {
            if (!_isOnTheGround) return;          

            var inputMovementX = _controls.Player.MovementX.ReadValue<float>();
            var inputMovementZ = _controls.Player.MovementY.ReadValue<float>();

            var directionX = transform.right * inputMovementX;
            var directionZ = transform.forward * inputMovementZ;

            _direction = directionX + directionZ;
            _targetVelocity = _direction * _movementSpeed;

            _localDirection = new(inputMovementX, 0, inputMovementZ);
        }

        private void SetTransformForwardVector() 
        {
            if (!_isOnTheGround) return;

            var forwardVector = new Vector3(_playerMovementInfo.DirectionAxisZ.x, 0, _playerMovementInfo.DirectionAxisZ.z);
            transform.forward = Vector3.MoveTowards(transform.forward, forwardVector, Time.fixedDeltaTime * _ratationForwardVectorSpeed);
        }

        private void CalculateRotation()
        {
            if (!_isOnTheGround) return;

            if (_direction != Vector3.zero) 
            {
                _targetRotation = Quaternion.LookRotation(_localDirection);
            }
        }

        private void CalculateJumpVelocity()
        {
            _isOnTheGround = Physics.Raycast(_raycastTransform.position, Vector3.down, _jumpRaycstDistance, _groundLayer);

            if (_isOnTheGround) 
            {
                _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);

                if (_controls.Player.Jump.ReadValue<float>() > 0)
                    _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _jumpVelocity, _rigidbody.velocity.z);
            }
        }
    }
}