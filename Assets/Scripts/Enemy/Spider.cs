using System.Collections;
using UnityEngine;

public class Spider : Enemy
{
    [SerializeField] private GameObject     _acidPrefab;
    [SerializeField] private Transform      _firePoint;
    [SerializeField] private float          _waitTimeBeforeAttack;

    public override void InitData()
    {
        base.InitData();
        speed = 3f;
        StartCoroutine(IdleToAttackRoutine());
    }

    public override void Update()
    {
        Move();
    }

    public override void Move() { }
    public override void OnDamage(int amount)
    {
        if (isDead) return;
        health -= amount;

        if (health <= 0)
        {
            enemyAnim.SetTrigger("dead");
            DropLoot();
            col.enabled = false;
        }
    }

    public void Attack()
    {
        //Instantiate Acid
        var acid = Instantiate(_acidPrefab, _firePoint.position, Quaternion.identity);
        acid.transform.parent = transform;
    }

    public void OnAttackStateExit()
    {
        StartCoroutine(IdleToAttackRoutine());
    }

    private IEnumerator IdleToAttackRoutine()
    {
        yield return new WaitForSeconds(_waitTimeBeforeAttack);
        enemyAnim.SetTrigger("attack");
    }
}
