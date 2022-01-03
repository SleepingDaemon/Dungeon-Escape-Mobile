using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy
{
    [SerializeField] private GameObject acidPrefab;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float waitTimeBeforeAttack;

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
        health -= amount;

        if (health <= 0)
        {
            enemyAnim.SetTrigger("dead");
            //Destroy(this.gameObject, 3f);
        }
    }

    public void Attack()
    {
        //Instantiate Acid
        var acid = Instantiate(acidPrefab, _firePoint.position, Quaternion.identity);
        acid.transform.parent = transform;
    }

    public void OnAttackStateExit()
    {
        StartCoroutine(IdleToAttackRoutine());
    }

    private IEnumerator IdleToAttackRoutine()
    {
        yield return new WaitForSeconds(waitTimeBeforeAttack);
        enemyAnim.SetTrigger("attack");
    }
}
