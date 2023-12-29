using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpFloor : ObstacleBase
{
    [SerializeField]
    private float jumpForce;

    private void OnEnable()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }
    protected override void CrashObj()
    {
        if (target == null) { return; }
        target.SetJumpForce(jumpForce);
    }
}
