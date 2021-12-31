using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 4f;
    [SerializeField] private float _jumpHeight = 10f;
    [SerializeField] private bool _isJumping = false;

    private bool _isGrounded;
    private Rigidbody2D _rb2D;
    private Grounding _grounding;

    private void Awake()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        _grounding = GetComponent<Grounding>();
    }

    private void Update()
    {
        _isGrounded = _grounding.IsGrounded();
    }

    private void FixedUpdate()
    {

        float x = Input.GetAxisRaw("Horizontal");

        if (_isGrounded)
        {
            JumpHandle();
        }

        _rb2D.velocity = new Vector3(x * _speed, _rb2D.velocity.y, 0);
    }

    private void JumpHandle()
    {
        if (_isJumping) _isJumping = false;

        if (Input.GetKeyDown(KeyCode.Space) && !_isJumping)
        {
            _isJumping = true;
            _rb2D.velocity = new Vector3(_rb2D.velocity.x, _jumpHeight);
        }
    }
}
