using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAnimationEvent : MonoBehaviour
{
    private Spider _spider;

    private void Awake()
    {
        _spider = GetComponentInParent<Spider>();
    }

    public void Fire()
    {
        print("Spider should fire!");
        _spider.Attack();
    }
}
