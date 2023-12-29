using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBase : ObjBase
{
    [SerializeField]
    protected int damage;
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            target = col.gameObject.GetComponent<PlayerMove>();
            Vector3 refdir = col.contacts[0].normal;
            CrashObj();
            target.ReflectFloor(refdir);
        }
    }
}
