using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObstacleBase : ObjBase
{
    [SerializeField]
    protected int damage;
    [SerializeField]
    protected Vector2 endSize;
    [SerializeField]
    protected float time;

    private bool isDestory = false;

    private void Awake()
    {
        endSize = new Vector2(0.1f, 0.1f);
        time = 1.5f;
    }

    private void Update()
    {
        if(isDestory)
        {
            if(transform.localScale.x<=0.11f)
            {
                this.gameObject.SetActive(false);
            }
        }
    }
    public void Bomb()
    {
        isDestory = true;
        transform.DOScale(endSize, time).SetEase(Ease.OutQuart);
        GetComponent<Collider2D>().enabled = false;
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            target = col.gameObject.GetComponent<PlayerMove>();
            Vector3 refdir = col.contacts[0].normal;
            target.ReflectFloor(refdir);
            CrashObj();
        }
    }
}
