using System.Collections;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private int _health                = 4;
    [SerializeField] private float _speed               = 4f;
    [SerializeField] private float _jumpHeight          = 10f;
    [SerializeField] private bool _isJumping            = false; //for the animator
    [SerializeField] private bool _enableJump           = false;
    [SerializeField] private bool _canDoubleJump        = false;
    [SerializeField] private bool _enableDoubleJump     = false;
    [SerializeField] private float _timeBetweenAttack   = 1f;

    private bool _isGrounded;
    private bool _isAttacking   = false;
    private bool _onHit         = false;
    private bool _isDead        = false;
    private Rigidbody2D _rb2D;
    private Grounding _grounding;
    private CharacterAudioHelper _audio;
    private PlayerAnimation _anim;
    private float xMove;

    public int Health { get => _health; set => _health = value; }

    private void Awake()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        _grounding = GetComponent<Grounding>();
        _anim = GetComponent<PlayerAnimation>();
        _audio = GetComponentInChildren<CharacterAudioHelper>();
    }

    private void Update()
    {
        if (_isDead) return;
        if (_anim.OnHitState()) return;

        _isGrounded = _grounding.IsGrounded();
        _anim.IsGrounded(_isGrounded);

        if (_isAttacking || _onHit || _isDead || GameManager.Instance.GetGameState() == GameState.INTRO
                                              || GameManager.Instance.GetGameState() == GameState.SHOP
                                              || GameManager.Instance.GetGameState() == GameState.COMPLETE
                                              || GameManager.Instance.GetGameState() == GameState.PAUSE)
        {
            _onHit = false;
            if(_rb2D.velocity != Vector2.zero)
            {
                _rb2D.velocity = Vector2.zero;
                _anim.Move(0);
            }

            return;
        }

#if UNITY_IOS
        xMove = CrossPlatformInputManager.GetAxis("Horizontal");
#elif UNITY_ANDROID
		xMove = CrossPlatformInputManager.GetAxis("Horizontal");
#elif UNITY_STANDALONE_WIN
        xMove = Input.GetAxisRaw("Horizontal");
#elif UNITY_WEBGL
        xMove = Input.GetAxisRaw("Horizontal");
#endif

        FlipSpritesOnMoveDirection(xMove);

        if (_isGrounded)
        {
            _isJumping = false;
            _anim.Jump(false);

            //jump if only grounded
            if (Input.GetKeyDown(KeyCode.Space) || CrossPlatformInputManager.GetButtonDown("Jump"))
            {
                _enableJump = true;
                _canDoubleJump = true;
            }
        }
        else
        {
            _isJumping = true;
            _anim.Jump(true);

            //double jump if only has boots of flight
            if (Input.GetKeyDown(KeyCode.Space) || CrossPlatformInputManager.GetButtonDown("Jump"))
            {
                if(_canDoubleJump && GameManager.Instance.HasBootsOfFlight)
                {
                    _enableDoubleJump = true;
                    _canDoubleJump = false;
                    _audio.PlayJumpSound();
                }
            }
        }

        if (!_onHit && GameManager.Instance.GetGameState() != GameState.INTRO
                    || GameManager.Instance.GetGameState() != GameState.SHOP
                    || GameManager.Instance.GetGameState() == GameState.COMPLETE
                    || GameManager.Instance.GetGameState() == GameState.PAUSE)
        {
            Move(xMove);
            _anim.Move(xMove);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.Instance.PauseMenu();
        }

        if (CrossPlatformInputManager.GetButtonDown("Attack") || Input.GetKeyDown(KeyCode.Mouse0) && _isGrounded && !_isAttacking)
        {
            StartCoroutine(AttackRoutine());
        }
    }

    private void FixedUpdate()
    {
        if (_isDead) return;
        if (_anim.OnHitState()) return;

        if (_enableJump)
        {
            HandleJump();
            _enableJump = false;
        }

        if (_enableDoubleJump)
        {
            HandleDoubleJump();
            _enableDoubleJump = false;
        }
    }

    private void Move(float x)
    {
        _rb2D.velocity = new Vector3(x * _speed, _rb2D.velocity.y, 0);
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

    private void HandleJump()
    {
        _isJumping = true;
        _rb2D.velocity = Vector3.up * _jumpHeight;
    }

    private void HandleDoubleJump()
    {
        _isJumping = true;
        _rb2D.velocity = Vector3.up * (_jumpHeight + 1);
        _canDoubleJump = false;
    }

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
    public void AddHealth(int amount)
    {
        _health = amount;
        UIManager.Instance.UpdateLives(_health);
    }
}
