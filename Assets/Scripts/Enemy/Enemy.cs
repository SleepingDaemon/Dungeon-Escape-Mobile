using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] protected int health;
    [SerializeField] protected float speed;
    [SerializeField] protected int gems;
    [SerializeField] protected Transform pointA, pointB;

    protected Animator enemyAnim;
    protected SpriteRenderer enemySprite;
    protected Vector3 currentTarget;

    public int Health { get => health; set => health = value; }

    public virtual void Awake()
    {
        InitComponents();
    }

    public virtual void Start()
    {
        InitData();
    }

    public virtual void Update()
    {
        if (enemyAnim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))    return;
        if (enemyAnim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))     return;

        Move();
    }

    //Initialize All References to Components
    public virtual void InitComponents()
    {
        enemyAnim = GetComponentInChildren<Animator>();
        enemySprite = GetComponentInChildren<SpriteRenderer>();
    }

    //Initialize Data from Child Classes
    public virtual void InitData()
    {
        //base
    }

    //Move all enemies from Point A to B.
    public virtual void Move()
    {
        if (currentTarget == pointA.position)
            enemySprite.flipX = true;
        else if (currentTarget == pointB.position)
            enemySprite.flipX = false;

        if (transform.position == pointA.position)
        {
            currentTarget = pointB.position;
            enemyAnim.SetTrigger("idle");
        }
        else if (transform.position == pointB.position)
        {
            currentTarget = pointA.position;
            enemyAnim.SetTrigger("idle");
        }

        transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
    }

    public virtual void OnDamage(int amount)
    {
        health -= amount;
        enemyAnim.SetTrigger("hit");

        if(health <= 0)
        {
            print("Enemy: " + this.gameObject.name + " died!");
            Destroy(this.gameObject);
        }
    }
}
