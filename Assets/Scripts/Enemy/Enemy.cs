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
        Vector3 _flipXpos = transform.localScale;

        if (currentTarget == pointA.position)
            _flipXpos.x = -1;
        else if (currentTarget == pointB.position)
            _flipXpos.x = 1;

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

        //Resume back to walking when not in combat
        var _distance = Vector3.Distance(player.transform.position, transform.position);
        if (_distance > 3)
        {
            _isHit = false;
            if (enemyAnim != null)
                enemyAnim.SetBool("inCombat", false);
        }

        //When in combat, face the player direction
        if (enemyAnim.GetBool("inCombat"))
        {
            direction = player.transform.position - transform.position;
            
            if (direction.x > 0)
            {
                // face left
                //enemySprite.flipX = false;
                _flipXpos.x = 1;
            }
            else if (direction.x < 0)
            {
                // face right
                //enemySprite.flipX = true;
                _flipXpos.x = -1;
            }
        }

        transform.localScale = _flipXpos;
    }

    public virtual void OnDamage(int amount)
    {
        print("Attacked: " + this.gameObject.name);
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
