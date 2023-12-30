using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TitlePlayer : MonoBehaviour
{
    private TitleManager manager_Title;
    public Transform point;
    Rigidbody2D rigid;
    Animator anim;

    public GameObject hell;

    public GameObject[] enemies;
    public int eCount = 0;
    public float createTime = 0;

    public float limitY = -5f;

    public float JumpForce;

    private bool isDash = true;
    private bool isEasteEgg = false;
    private Vector2 initPos;
    private void Awake()
    {
        manager_Title = FindObjectOfType<TitleManager>();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        initPos = transform.position;
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
            if(isDash == false)
            {
                rigid.velocity = new Vector2(-1, -1).normalized * 12f;
                isDash = true;
            }
        }

        if(transform.position.y <= limitY)
        {
            //페이드 아웃
            manager_Title.MobDown();
            gameObject.SetActive(false);
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
        if(isEasteEgg == true)
        {
            Invoke("Bye", 1.5f);
            return;
        }
        StartCoroutine(CreateEnemy());
    }

    public void Dive()
    {
        hell.SetActive(false);
        isDash = false;
        anim.SetBool("PickUp", false);
        JumpForce = 6f;
        Jump();
    }

    IEnumerator CreateEnemy()
    {
        createTime = Time.time;
        while(true)
        {
            if (Time.time - 1f > createTime)
            {
                manager_Title.MobAppear();
                hell.SetActive(true);
                hell.transform.position = transform.position + new Vector3(0, 1.2f);
                yield break;
                //if (eCount > enemies.Length - 1)
                //{
                //    anim.SetBool("PickUp", false);
                //    Dive();
                //    yield break;
                //}
                //createTime = Time.time;
                //enemies[eCount].SetActive(true);
                //eCount++;
            }
            yield return null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            if (isEasteEgg) { return; }
            GetComponent<SpriteRenderer>().flipX = true;
            rigid.velocity = new Vector2(1, -1).normalized * 10f;
        }
    }


    //이스터에그
    public void GoToZem()
    {
        isEasteEgg = true;
        transform.DOMove(new Vector2(point.position.x, transform.position.y), 1f);
    }
    void Bye()
    {
        rigid.gravityScale = 0;
        rigid.drag = 0f;
        transform.DOMove(new Vector2(4f, transform.position.y), 4f);
        GetComponent<SpriteRenderer>().flipX = true;
    }
    public void End()
    {
        isEasteEgg = false;
        transform.position = initPos;
        GetComponent<SpriteRenderer>().flipX = false;
        manager_Title.EndEasteEgg();
        point.gameObject.SetActive(true);
        rigid.gravityScale = 1;
        rigid.drag = 1.5f;
    }
}
