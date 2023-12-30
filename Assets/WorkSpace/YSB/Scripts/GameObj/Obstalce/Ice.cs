using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : ObstacleBase
{
    Rigidbody2D rigid;
    [SerializeField]
    float moveSpeed;

    [SerializeField]
    float limitY = 0;

    bool falling;

    public void OnTriggerTouch()
    {
        falling = true;
        rigid.velocity = Vector2.down * moveSpeed;
        limitY = transform.position.y - 20;
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
