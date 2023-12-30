using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : ObstacleBase
{
    private void OnEnable()
    {
        //transform.localScale = new Vector3(1, 1, 1);
    }
    protected override void CrashObj()
    {
        if(target == null) { return; }
        target.TakeDamage(damage);
        target.SetJumpForce();
    }
}
