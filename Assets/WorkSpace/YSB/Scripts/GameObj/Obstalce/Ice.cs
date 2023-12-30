using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Ice : ObstacleBase
{
    Rigidbody2D rigid;
    [SerializeField]
    float moveSpeed;
    [SerializeField]
    protected float delay;

    [SerializeField]
    float limitY = 0;

    bool falling;

    public void OnTriggerTouch()
    {
        if (!falling)
        {
            transform.DORotate(Vector2.zero, delay).OnComplete(() =>
            {
                StartCoroutine(DelayNFall());
            });
        }
    }

    private void Awake()
    {
        base.Awake();
        rigid = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (falling &&
            transform.position.y < limitY)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
    IEnumerator DelayNFall()
    {
        yield return new WaitForSeconds(delay);
        falling = true;
        rigid.velocity = Vector2.down * moveSpeed;
        limitY = transform.position.y - 20;
    }

    protected override void CrashObj()
    {
        if (target == null) { return; }
        //GameManager.instance.MinusTime(damage);
        target.fall();
        gameObject.SetActive(false);
        target.TakeDamage(damage);
        //target.SetJumpForce();
    }
}
