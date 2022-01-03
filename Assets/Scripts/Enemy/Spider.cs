using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy
{
    public override void InitData()
    {
        base.InitData();
        speed = 3f;
    }
}
