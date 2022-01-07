using System.Collections;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private int _health                = 4;
    [SerializeField] private float _speed               = 4f;
    [SerializeField] private float _jumpHeight          = 10f;
    [SerializeField] private bool _isJumping            = false;
    [SerializeField] private bool _canDoubleJump        = false;
    [SerializeField] private float _timeBetweenAttack   = 1f;
    private bool _isJumpPressed = false;
    private bool _isGrounded;
    private bool _isAttacking   = false;
    private bool _onHit         = false;
    private bool _isDead        = false;
    private Rigidbody2D _rb2D;
    private Grounding _grounding;
    private PlayerAnimation _anim;
    private float yVelocity;

    public int Health { get => _health; set => _health = value; }

    private void Awake()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        _grounding = GetComponent<Grounding>();
        _anim = GetComponent<PlayerAnimation>();
    }

    private void Update()
    {
        _isGrounded = _grounding.IsGrounded();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.Instance.PauseMenu();
        }

        if (_isDead) return;
        if (_anim.OnHitState()) return;

        _isJumpPressed = Input.GetKeyDown(KeyCode.Space) || CrossPlatformInputManager.GetButtonDown("Jump");

        if (CrossPlatformInputManager.GetButtonDown("Attack") && _isGrounded && !_isAttacking)
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

        float xMove = CrossPlatformInputManager.GetAxis("Horizontal");

        FlipSpritesOnMoveDirection(xMove);

        if (_isGrounded)
        {
            _isJumping = false;
            _anim.Jump(false);

            //HandleJumping();
            if (_isJumpPressed)
            {
                _rb2D.velocity = new Vector3(_rb2D.velocity.x, _jumpHeight);
                _canDoubleJump = true;
                //_isJumpPressed = false;
            }
        }
        else
        {
            _isJumping = true;

            //HandleDoubleJumping();
            if (_isJumpPressed && _canDoubleJump && GameManager.Instance.HasBootsOfFlight)
            {
                _rb2D.velocity = new Vector3(_rb2D.velocity.x, _jumpHeight, 0);
                _canDoubleJump = false;
                _isJumpPressed = false;
            }

            _anim.Jump(true);
        }

        if (!_onHit)
        {
            _rb2D.velocity = new Vector3(xMove * _speed, _rb2D.velocity.y, 0);
        }

        _anim.Move(xMove);
    }

    private void FlipSpritesOnMoveDirection(float xMove)
    {
        Vector3 _flipPlayerPos = transform.localScale;

        if (xMove > 0.01f)
        {
            _flipPlayerPos.x = 1;
        }
        else if (xMove < -0.01f)
        {
            _flipPlayerPos.x = -1;
        }

        transform.localScale = _flipPlayerPos;
    }

    private void HandleJumping()
    {

    }

    private void HandleDoubleJumping()
    {

    }

    public Rigidbody2D GetRigidBody() => _rb2D;

    private IEnumerator AttackRoutine()
    {
        _isAttacking = true;
        _anim.Attack();
        yield return new WaitForSeconds(_timeBetweenAttack);
        _isAttacking = false;
    }

    public void OnDamage(int amount)
    {
        if (_isDead) return;
        _health -= amount;
        UIManager.Instance.UpdateLives(_health);
        _onHit = true;
        _anim.Hit();

        if(_health <= 0)
        {
            //Game over
            _anim.Death();
            _isDead = true;
            GameManager.Instance.GameOver();
        }
    }

    public bool IsPlayerDead() => _isDead;
}
