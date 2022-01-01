using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int _health;
    [SerializeField] protected float _speed;
    [SerializeField] protected int _gems;
    [SerializeField] protected Transform _pointA, _pointB;

    private Animator _enemyAnim;
    private SpriteRenderer _enemySprite;
    private Vector3 _currentTarget;
    protected bool _isSwitching = false;

    private void Awake()
    {
        _enemyAnim = GetComponentInChildren<Animator>();
        _enemySprite = GetComponentInChildren<SpriteRenderer>();
    }

    public virtual void Update()
    {
        if (_enemyAnim.GetCurrentAnimatorStateInfo(0).IsName("Idle")) return;

        Move();
    }

    public virtual void Attack()
    {
        Debug.Log("Base Attack");
    }

    public virtual void Move()
    {
        if (_currentTarget == _pointA.position)
            _enemySprite.flipX = true;
        else if (_currentTarget == _pointB.position)
            _enemySprite.flipX = false;

        if (transform.position == _pointA.position)
        {
            _currentTarget = _pointB.position;
            _enemyAnim.SetTrigger("idle");
        }
        else if (transform.position == _pointB.position)
        {
            _currentTarget = _pointA.position;
            _enemyAnim.SetTrigger("idle");
        }

        transform.position = Vector3.MoveTowards(transform.position, _currentTarget, _speed * Time.deltaTime);
    }
}
