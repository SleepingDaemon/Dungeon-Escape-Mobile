using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] protected int health;
    [SerializeField] protected float speed;
    [SerializeField] protected int gems;
    [SerializeField] protected Transform pointA, pointB;

    protected bool _isHit = false;
    protected Animator enemyAnim;
    protected SpriteRenderer enemySprite;
    protected Vector3 currentTarget;
    protected Vector3 direction;
    protected Player player;

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
        if (enemyAnim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && !enemyAnim.GetBool("inCombat")) return;

        Move();
    }

    //Initialize All References to Components
    public virtual void InitComponents()
    {
        enemyAnim = GetComponentInChildren<Animator>();
        enemySprite = GetComponentInChildren<SpriteRenderer>();
        player = GameObject.Find("Player").GetComponent<Player>();
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

        if(!_isHit)
            transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);

        var _distance = Vector3.Distance(player.transform.position, transform.position);
        if (_distance > 2)
        {
            _isHit = false;
            if (enemyAnim != null)
                enemyAnim.SetBool("inCombat", false);
        }

        if (enemyAnim.GetBool("inCombat"))
        {
            direction = player.transform.position - transform.localPosition;
            if (direction.x > 0)
            {
                // face left
                enemySprite.flipX = false;
            }
            else if (direction.x < 0)
            {
                // face right
                enemySprite.flipX = true;
            }
        }
    }

    public virtual void OnDamage(int amount)
    {
        health -= amount;
        enemyAnim.SetTrigger("hit");
        _isHit = true;
        enemyAnim.SetBool("inCombat", true);
        


        if(health <= 0)
        {
            print("Enemy: " + this.gameObject.name + " died!");
            Destroy(this.gameObject);
        }
    }
}
