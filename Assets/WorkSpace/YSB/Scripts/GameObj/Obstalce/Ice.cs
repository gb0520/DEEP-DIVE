using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour
{
    Rigidbody2D rigid;
    Vector2 dir;
    [SerializeField]
    float moveSpeed;

    float limitY = 0;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        dir = Vector2.zero;
    }
    private void OnDisable()
    {
        dir = Vector2.zero;
    }
    //public void SetIce(Vector2 pos)
    //{
    //    transform.position = pos;
    //    float dirx;
    //    if (transform.position.x < 0)
    //    {
    //        dirx = Random.Range(0, 0.5f);
    //    }
    //    else
    //    {
    //        dirx = Random.Range(-0.5f, 0);
    //    }

    //    dir = new Vector2(dirx, -1).normalized;
    //    limitY = pos.y - 20f;

    //    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    //    Debug.Log(angle);
    //    Quaternion degree = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    //    transform.rotation = degree;
    //}

    private void Update()
    {
        Move();
    }
    private void Move()
    {
        if(dir == Vector2.zero) { return; }
        rigid.velocity = dir * moveSpeed;

        if (transform.position.y < limitY)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponentInParent<PlayerMove>().fall();
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            dir = Vector2.down;
            limitY = collision.transform.position.y - 20f;
        }
    }
}
