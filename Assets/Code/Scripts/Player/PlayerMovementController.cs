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

        [Header("Jumping")]
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private float _jumpRaycstDistance;
        [SerializeField] private float _jumpVelocity;
        [SerializeField] private Transform _raycastTransform;

        private Controls _controls;
        private Vector3 _targetVelocity = Vector3.zero;
        private Vector3 _direction = Vector3.zero;
        private Quaternion _targetRotation = Quaternion.identity;

        private bool _isOnTheGround = false;

        public float MovementSpeed => _movementSpeed;
        public Rigidbody Rigidbody => _rigidbody;

        [Inject]
        public void Construct(Controls controls) 
        {
            _controls = controls;
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

            var currenVelocity = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);
            currenVelocity = Vector3.MoveTowards(currenVelocity, _targetVelocity, _velocitySpeed * Time.fixedDeltaTime);
            _rigidbody.velocity = new Vector3(currenVelocity.x, _rigidbody.velocity.y, currenVelocity.z);

            transform.rotation = Quaternion.RotateTowards(transform.rotation,
                _targetRotation, Time.fixedDeltaTime * _ratationSpeed);
        }     

        private void CalculateVelocity() 
        {
            if (!_isOnTheGround) return;         

            var xMovement = _controls.Player.MovementX.ReadValue<float>();
            var zMovement = _controls.Player.MovementY.ReadValue<float>();

            _direction = new Vector3(xMovement, _direction.y, zMovement);

            _targetVelocity = _direction * _movementSpeed;
        }

        private void CalculateRotation()
        {
            if (!_isOnTheGround) return;

            if (_direction != Vector3.zero)
                _targetRotation = Quaternion.LookRotation(_direction);
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