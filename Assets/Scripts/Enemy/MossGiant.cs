using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossGiant : Enemy
{
    private void Start()
    {
        
    }

    public override void Attack()
    {
        base.Attack();
        Debug.Log("MossGiant is Attacking!");
    }

    public override void Update()
    {
        base.Update();
    }
}
