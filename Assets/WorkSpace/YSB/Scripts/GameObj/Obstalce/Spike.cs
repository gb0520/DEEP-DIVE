using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : ObstacleBase
{
    protected override void CrashObj()
    {
        if(target == null) { return; }
        target.TakeDamage(damage);
        target.SetJumpForce();
    }
}
