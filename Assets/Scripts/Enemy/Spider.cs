using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy
{
    [SerializeField] private GameObject acidPrefab;
    [SerializeField] private Transform _firePoint;

    public override void InitData()
    {
        base.InitData();
        speed = 3f;
    }

    public override void Move()
    {
        
    }

    public void Attack()
    {
        //Instantiate Acid
        var acid = Instantiate(acidPrefab, _firePoint.position, Quaternion.identity);
        acid.transform.parent = transform;
    }
}
