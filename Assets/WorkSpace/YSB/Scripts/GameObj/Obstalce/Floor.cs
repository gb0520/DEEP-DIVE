using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : ObstacleBase
{
    protected override void CrashObj()
    {
        target.SetJumpForce();
    }

    //private void OnCollisionEnter2D(Collision2D col)
    //{
    //    if (col.gameObject.CompareTag("Player"))
    //    {
    //        Debug.Log("qkekr°ú Ãæµ¹");
    //        Vector3 refdir = col.contacts[0].normal;
    //        col.gameObject.SendMessage("ReflectFloor", refdir);
    //    }
    //}
}
