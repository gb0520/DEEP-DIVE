using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitlePlayer : MonoBehaviour
{
    public Transform point;
    Rigidbody2D rigid;
    Animator anim;

    public GameObject[] enemies;
    public int eCount = 0;
    public float createTime = 0;

    public float limitY = -5f;

    public float JumpForce;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        //Invoke("JumpMove", 1f);
    }
    private void Update()
    {
        
        if(rigid.velocity.y < 0f)
        {
            anim.SetBool("isJump", false);
        }

        if(transform.position.y <= limitY)
        {
            //ÆäÀÌµå ¾Æ¿ô
        }
    }

    public void JumpMove()
    {
        float dis = Vector2.Distance(point.position, transform.position);
        float vel = dis / Mathf.Sin(3 * (45 * Mathf.Deg2Rad) / 9.8f);
        JumpForce = vel;
        Jump();
    }
    public void Jump()
    {
        Vector2 dir = new Vector2(-1, 1).normalized;
        anim.SetBool("isJump", true);
        rigid.AddForce(dir * JumpForce, ForceMode2D.Impulse);
    }

    public void PickUp()
    {
        anim.SetBool("PickUp", true);
        StartCoroutine(CreateEnemy());
    }

    public void Dive()
    {
        JumpForce = 5f;
        Jump();
    }

    IEnumerator CreateEnemy()
    {
        createTime = Time.time;
        while(true)
        {
            if (Time.time - 1.5f > createTime)
            {
                if (eCount > enemies.Length - 1)
                {
                    anim.SetBool("PickUp", false);
                    Dive();
                    yield break;
                }
                createTime = Time.time;
                enemies[eCount].SetActive(true);
                eCount++;
            }
            yield return null;
        }
    }
}
