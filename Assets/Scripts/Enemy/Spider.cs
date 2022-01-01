using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy
{
    public override void Attack()
    {
        base.Attack();
        Debug.Log("Spider is Attacking!");
    }

    public override void Update()
    {
        
    }
}
