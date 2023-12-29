using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpFloor : ObstacleBase
{
    [SerializeField]
    private float jumpForce;
    protected override void CrashObj()
    {
        if (target == null) { return; }
        target.SetJumpForce(jumpForce);
    }
}
