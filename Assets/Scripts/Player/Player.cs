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
    private bool _isAttacking = false;
    private Rigidbody2D _rb2D;
    private SpriteRenderer _sprite;
    private SpriteRenderer _swordArcSprite;
    private Animator _swordArcAnim;
    private Grounding _grounding;
    private PlayerAnimation _anim;

    private void Awake()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        _grounding = GetComponent<Grounding>();
        _anim = GetComponent<PlayerAnimation>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
        _swordArcSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
        _swordArcAnim = transform.GetChild(1).GetComponent<Animator>();
    }

    private void Update()
    {
        _isGrounded = _grounding.IsGrounded();

        //Enable Jumping when hitting the space key
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _enableJump = true;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && _isGrounded)
        {
            StartCoroutine(AttackRoutine());
        }
    }

    private void FixedUpdate()
    {
        if (_isAttacking)
        {
            _rb2D.velocity = Vector2.zero;
            return;
        }

        float xMove = Input.GetAxisRaw("Horizontal");

        FlipSpritesOnMoveDirection(xMove);

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

    private void FlipSpritesOnMoveDirection(float xMove)
    {
        Vector3 _swordArcPos = _swordArcSprite.transform.localPosition;
        Vector3 _swordArcRot = _swordArcSprite.transform.localEulerAngles;

        if (xMove > 0.01f)
        {
            _swordArcPos.x = 0.5f;
            _swordArcRot.x = 66f;
            _swordArcSprite.flipY = false;
            _sprite.flipX = false;
        }
        else if (xMove < -0.01f)
        {
            _swordArcPos.x = -0.5f;
            _swordArcRot.x = -66f;
            _swordArcSprite.flipY = true;
            _sprite.flipX = true;
        }

        _swordArcSprite.transform.localPosition = _swordArcPos;
        _swordArcSprite.transform.localEulerAngles = _swordArcRot;
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

    private IEnumerator AttackRoutine()
    {
        _isAttacking = true;
        _anim.Attack();
        yield return new WaitForSeconds(0.47f);
        _isAttacking = false;
    }
}
