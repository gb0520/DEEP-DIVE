using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : ItemBase
{
    protected override void CrashObj()
    {
        target.Bomb();
        this.gameObject.SetActive(false);
    }
}
