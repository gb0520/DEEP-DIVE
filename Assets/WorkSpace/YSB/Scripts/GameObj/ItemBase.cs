using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : ObjBase
{
    [SerializeField]
    protected int heal;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            target = col.gameObject.GetComponent<PlayerMove>();
            CrashObj();
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            target = col.gameObject.GetComponent<PlayerMove>();
            CrashObj();
        }
    }
}
