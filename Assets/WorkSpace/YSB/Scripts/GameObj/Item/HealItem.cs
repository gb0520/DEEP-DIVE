using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : ItemBase
{
    protected override void CrashObj()
    {
        target.TakeHp(heal);
    }
}
