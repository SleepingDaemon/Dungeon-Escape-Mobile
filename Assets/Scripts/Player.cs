using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 4f;
    [SerializeField] private float _jumpHeight = 10f;
    [SerializeField] private bool _isJumping = false;
    private bool _enableJump = false;
    private bool _isGrounded;
    private Rigidbody2D _rb2D;
    private SpriteRenderer _sprite;
    private Grounding _grounding;
    private PlayerAnimation _anim;

    private void Awake()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        _grounding = GetComponent<Grounding>();
        _anim = GetComponent<PlayerAnimation>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        _isGrounded = _grounding.IsGrounded();
        print(_isJumping);

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _enableJump = true;
        }
    }

    private void FixedUpdate()
    {

        float xMove = Input.GetAxisRaw("Horizontal");

        FlipSpriteOnMoveDirection(xMove);

        if (_isGrounded)
        { 
            HandleJumping();
        }

        if (!_isGrounded)
        {
            _isJumping = true;
            _anim.Jump(true);
        }

        _rb2D.velocity = new Vector3(xMove * _speed, _rb2D.velocity.y, 0);
        _anim.Move(xMove);
    }

    private void FlipSpriteOnMoveDirection(float xMove)
    {
        if (xMove > 0.01f)
            _sprite.flipX = false;
        else if (xMove < -0.01f)
            _sprite.flipX = true;
    }

    private void HandleJumping()
    {
        _isJumping = false;
        _anim.Jump(false);

        if (_enableJump)
        {
            _rb2D.velocity = new Vector3(_rb2D.velocity.x, _jumpHeight);
            _enableJump = false;
        }
    }

    public Rigidbody2D GetRigidBody() => _rb2D;
}
