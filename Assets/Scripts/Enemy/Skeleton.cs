using System;
using System.Collections;
using UnityEngine;

public class Skeleton : Enemy
{
    [Header("Revival Properties")]
    [SerializeField] private float _minMinutesForRevival = 120f;
    [SerializeField] private float _maxMinutesForRevival = 240f;
    [SerializeField] private float _randomTime;
    [SerializeField] private bool _revive = false;

    public override void InitData()
    {
        base.InitData();
        speed = 0.6f;
    }

    public override void Update()
    {
        if(!_revive)
            base.Update();

        if (isDead)
        {
            isDead = false;
            _revive = true;
            StartCoroutine(ReviveRoutine());
        }
    }

    IEnumerator ReviveRoutine()
    {
        _randomTime = UnityEngine.Random.Range(_minMinutesForRevival, _maxMinutesForRevival);
        yield return new WaitForSeconds(_randomTime);
        health = 5;
        col.enabled = true;
        enemyAnim.SetTrigger("revive");
        _revive = false;
    }

    public void TriggerIdle() => enemyAnim.SetTrigger("idle");
}
