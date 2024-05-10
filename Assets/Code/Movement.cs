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
        Vector3 movement = new Vector3(_horizontal, 0.0f, _vertical);
        transform.Translate(movement * _speed * Time.fixedDeltaTime);
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
            //_anim.SetBool("IsGround", true);
        }
        else
        {
            _isGrounded = false;
            _speed = _speedDefult / 2.5f;
            //_anim.SetBool("IsGround", false);
        }
    }
}
