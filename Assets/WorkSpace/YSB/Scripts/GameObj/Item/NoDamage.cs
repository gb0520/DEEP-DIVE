using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoDamage : ItemBase
{
    protected override void CrashObj()
    {
        target.NoDamage();
        
    }
}
