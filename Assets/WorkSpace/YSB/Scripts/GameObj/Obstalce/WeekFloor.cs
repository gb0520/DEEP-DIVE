using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeekFloor : ObstacleBase
{
    protected override void CrashObj()
    {
        target.SetJumpForce();
        Bomb();
    }
}
