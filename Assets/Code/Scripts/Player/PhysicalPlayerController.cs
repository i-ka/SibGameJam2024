using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Code.Scripts.Player
{
    public class PhysicalPlayerController : MonoBehaviour
    {
        //settings
        [SerializeField] private float maxSpeed = 20;
        [Range(0, 1)][SerializeField] private float groundedAccelerationRate = 1;
        [Range(0, 1)][SerializeField] private float airborneAccelerationRate = 0.1f;
        [SerializeField] private LayerMask groundLayers;
        [SerializeField] private float groundCheckRadius = 0.5f;
        [SerializeField] private float jumpForce;
        [SerializeField] private float jumpCooldown;
        [SerializeField] private Animator animator;
        [SerializeField] private Transform weaponTransform;
        public bool followCameraRotation = true;
        

        //components
        private Rigidbody _rigidBody;
        private PlayerInput _playerInput;
        [SerializeField]
        private Transform cameraTarget;
        [SerializeField] private Transform groundCheck;
    
        //inputs
        private Vector2 _move;
        private Vector2 _look;
        private bool _isJump;

        private float _cameraPitch;
        private float _cameraYaw;
        private bool _isGrounded;
        private float _jumpTimer;
        
        // animation IDs
        private readonly int _animIDSpeed = Animator.StringToHash("Speed");
        private readonly int _animIDGrounded = Animator.StringToHash("Grounded");
        private readonly int _animIDJump = Animator.StringToHash("Jump");
        private readonly int _animIDFreeFall = Animator.StringToHash("FreeFall");
        private readonly int _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
    
        private bool IsCurrentDeviceMouse => _playerInput.currentControlScheme == "KeyboardMouse";
    
        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody>();
            _playerInput = GetComponent<PlayerInput>();
        }

        private void Update()
        {
            UpdateCameraLook();
            UpdateWeaponRotation();
            UpdateTimers();
        }

        private void UpdateWeaponRotation()
        {
            weaponTransform.rotation = cameraTarget.rotation;
        }

        private void FixedUpdate()
        {
            UpdateRotation();
            CheckGround();
            Move();
            Jump();
        }

        private void UpdateCameraLook()
        {
            var multiplier = IsCurrentDeviceMouse ? 1 : Time.deltaTime;
            var multipliedLook = _look * multiplier;
        
            _cameraPitch += multipliedLook.y;
            _cameraYaw += multipliedLook.x;
        
            _cameraYaw = ClampAngle(_cameraYaw, float.MinValue, float.MaxValue);
            _cameraPitch = ClampAngle(_cameraPitch, -30, 70);
            cameraTarget.rotation = Quaternion.Euler(_cameraPitch, _cameraYaw, 0.0f);
        }

        private void UpdateRotation()
        {
            if (!followCameraRotation)
                return;
            var newRotation = Quaternion.Euler(0, cameraTarget.eulerAngles.y, 0);
            _rigidBody.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * 20);
        }

        private void CheckGround()
        {
            _isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayers, QueryTriggerInteraction.Ignore);
            animator.SetBool(_animIDGrounded, _isGrounded);
        }

        private void Move()
        {
            var cameraHorizontalLook = Quaternion.Euler(0, cameraTarget.rotation.eulerAngles.y, 0);
            var moveDirection = cameraHorizontalLook * new Vector3(_move.x, 0, _move.y).normalized;
        
            var targetVelocity = moveDirection * maxSpeed;
            var currentVelocity = _rigidBody.velocity;
            var currentHorizontalVelocity = new Vector3(currentVelocity.x, 0, currentVelocity.z);
            var accelerationRate = _isGrounded ? groundedAccelerationRate : airborneAccelerationRate;
            var movement = (targetVelocity - currentHorizontalVelocity) * accelerationRate;
            
            _rigidBody.AddForce(movement * maxSpeed, ForceMode.Acceleration);
            
            animator.SetFloat(_animIDSpeed, currentHorizontalVelocity.magnitude);
            animator.SetFloat(_animIDMotionSpeed, _move.magnitude);
        }

        private void Jump()
        {
            if (_isGrounded && _isJump && _jumpTimer <= 0)
            {
                _rigidBody.AddForce(transform.up * jumpForce, ForceMode.Acceleration);
                _jumpTimer = jumpCooldown;
                animator.SetTrigger(_animIDJump);
            }
            animator.SetBool(_animIDFreeFall, !_isGrounded);
        }

        private void UpdateTimers()
        {
            if (_jumpTimer >= 0)
                _jumpTimer -= Time.deltaTime;
        }
    
        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            _move = context.ReadValue<Vector2>();
        }
    
        public void OnLook(InputAction.CallbackContext context)
        {
            _look = context.ReadValue<Vector2>();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            _isJump = context.performed;
        }
    }
}
