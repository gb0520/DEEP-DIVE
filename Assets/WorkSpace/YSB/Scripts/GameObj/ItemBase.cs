using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : ObjBase
{
    [SerializeField]
    protected int heal;
    protected Vector3 initScale;
    protected void Awake()
    {
        initScale = transform.localScale;
        transform.localScale = initScale;
    }
    private void OnEnable()
    {
        transform.localScale = initScale;
        GetComponent<Collider2D>().enabled = true;
    }
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
