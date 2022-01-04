using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private int health = 5;
    [SerializeField] private float _speed = 4f;
    [SerializeField] private float _jumpHeight = 10f;
    [SerializeField] private bool _isJumping = false;
    [SerializeField] private float timeBetweenAttack = 1f;
    [SerializeField] private Collider2D playerCollider;
    [SerializeField] private int diamonds = 0;
    private bool _enableJump = false;
    private bool _isGrounded;
    private bool _isAttacking = false;
    private bool _onHit = false;
    private bool _isDead = false;
    private Rigidbody2D _rb2D;
    private SpriteRenderer _sprite;
    private SpriteRenderer _swordArcSprite;
    private Animator _swordArcAnim;
    private Grounding _grounding;
    private PlayerAnimation _anim;

    public int Health { get => health; set => health = value; }

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
        if (_isDead) return;
        if (_anim.OnHitState()) return;

        //Enable Jumping when hitting the space key
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _enableJump = true;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && _isGrounded && !_isAttacking)
        {
            StartCoroutine(AttackRoutine());
        }
    }

    private void FixedUpdate()
    {
        if (_isDead) return;

        if (_isAttacking || _onHit || _isDead)
        {
            _onHit = false;
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

        if(!_onHit)
            _rb2D.velocity = new Vector3(xMove * _speed, _rb2D.velocity.y, 0);
        _anim.Move(xMove);
    }

    private void FlipSpritesOnMoveDirection(float xMove)
    {
        //Vector3 _swordArcPos = _swordArcSprite.transform.localPosition;
        //Vector3 _swordArcRot = _swordArcSprite.transform.localEulerAngles;
        Vector3 _flipPlayerPos = transform.localScale;

        if (xMove > 0.01f)
        {
            //_swordArcPos.x = 0.5f;
            //_swordArcRot.x = 66f;
            //_swordArcSprite.flipY = false;
            //_sprite.flipX = false;
            _flipPlayerPos.x = 1;

        }
        else if (xMove < -0.01f)
        {
            //_swordArcPos.x = -0.5f;
            //_swordArcRot.x = -66f;
            //_swordArcSprite.flipY = true;
            //_sprite.flipX = true;
            _flipPlayerPos.x = -1;
        }

        //_swordArcSprite.transform.localPosition = _swordArcPos;
        //_swordArcSprite.transform.localEulerAngles = _swordArcRot;
        transform.localScale = _flipPlayerPos;
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
        yield return new WaitForSeconds(timeBetweenAttack);
        _isAttacking = false;
    }

    public void OnDamage(int amount)
    {
        health -= amount;
        _onHit = true;
        _anim.Hit();


        if(health <= 0)
        {
            //Game over
            _anim.Death();
            _isDead = true;
            //_rb2D.gravityScale = 0;
            //playerCollider.enabled = false;
        }
    }

    public void AddDiamond(int amount) => diamonds += amount;

    public bool IsPlayerDead() => _isDead;
}
