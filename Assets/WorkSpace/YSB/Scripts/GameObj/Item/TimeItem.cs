using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeItem : ItemBase
{
    protected override void CrashObj()
    {
        GameManager.instance.TakeTime(heal);
        gameObject.SetActive(false);
    }
}
