using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class Movement : MonoBehaviour
{
    private float _horizontal;
    private float _vertical;

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private LayerMask _notPlayerLayer;
    private float _speedDefult;
    private Rigidbody _rigidbody;
    private Animator _anim;

    private bool _isGrounded;

    private Vector3 _playerMovementInput;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        _speedDefult = _speed;
    }
    private void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
        jump();
    }
    private void FixedUpdate()
    {
        Move();
        checkGround();
    }

    private void Move()
    {
        _playerMovementInput = new Vector3(_horizontal, 0, _vertical);
        Vector3 MoveVector = transform.TransformDirection(_playerMovementInput) * _speed;
        _rigidbody.velocity = new Vector3(MoveVector.x, _rigidbody.velocity.y, MoveVector.z);
    }

    private void jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);

            _rigidbody.velocity = new Vector3(0, 0, 0);
        }
    }

    private void checkGround()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 0.3f, _notPlayerLayer))
        {
            _isGrounded = true;
            _speed = _speedDefult;
            _anim.SetBool("IsGround", true);
        }
        else
        {
            _isGrounded = false;
            _speed = _speedDefult / 8f;
            _anim.SetBool("IsGround", false);
        }
    }
}
