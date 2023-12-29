using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rigid;
    private Animator anim;
    private SpriteRenderer rend;

    private KeyCode leftKey = KeyCode.A;
    private KeyCode rightKey = KeyCode.D;

    private Vector3 preDirection;   //이전 방향 > 기억용
    private Vector3 curDirection;
    private Vector3 left = new Vector3(-1, -1, 0).normalized;
    private Vector3 right = new Vector3(1, -1, 0).normalized;

    public float YPos => transform.position.y;

    [SerializeField]
    private Vector2 nowSpeed;

    [SerializeField]
    private int maxHp;
    [SerializeField]
    private int hp;
    [SerializeField]
    private float moveSpeed = 0f;
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float dashSpeed;
    [SerializeField]
    private float dashTime;
    private bool isDashing = false;
    [SerializeField]
    private int dashCount = 1;

    private bool isCrashing = false;
    private bool isLoading = false;
    private float jumpForce;


    [SerializeField]
    private float bombSize;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        rend = GetComponentInChildren<SpriteRenderer>();
        curDirection = left;
        preDirection = curDirection;

        hp = maxHp;
    }
    private void Update()
    {
        if (Input.GetKeyDown(leftKey))
        {
            StartDash(left);
        }
        if (Input.GetKeyDown(rightKey))
        {
            StartDash(right);
        }

        SetRend(curDirection.x);

        if(rigid.velocity.y > 0)
        {
            if(isLoading == true) { return; }
            curDirection = new Vector3(curDirection.x, -curDirection.y, 0f);
        }

        if(isCrashing)
        {
            if (rigid.velocity.y <= 0)
            {
                rigid.gravityScale = 0f;
                rigid.drag = 0f;
                rigid.velocity = Vector3.zero;
                anim.SetBool("isJump", false);

                curDirection = new Vector3(curDirection.x, -curDirection.y, 0f);
                isCrashing = false;
            }
        }
        Move();        

        nowSpeed = rigid.velocity;
    }

    //----------------------------------------------Move
    private void Move()
    {
        if (isDashing == true || isCrashing == true || isLoading == true) { return; }
        moveSpeed += Time.deltaTime;
        if (moveSpeed >= maxSpeed)
        {
            moveSpeed = maxSpeed;
        }
        rigid.velocity = curDirection * moveSpeed;
    }


    private void StartDash(Vector3 dir)
    {
        if (dashCount <= 0 || isCrashing == true) { return; }
        isDashing = true;
        dashCount -= 1;
        curDirection = dir;
        rigid.velocity = curDirection * dashSpeed;
        StartCoroutine(Dash());
    }

    private IEnumerator Dash()
    {
        float timer = 0f;
        while (true)
        {
            timer += Time.deltaTime;
            if (timer >= dashTime)
            {
                isDashing = false;
                yield break;
            }
            yield return null;
        }
    }

    //----------------------------------------------Crash
    public void SetDirection(float x)  //x값으로 조절
    {
        anim.SetBool("isJump", false);
        if (x < 0)
        {
            curDirection = left;
        }
        else
        {
            curDirection = right;
        }
    }
    public void SetDirection(bool loading)  //전체 조정
    {
        isLoading = loading;
        if(isLoading == true)
        {
            preDirection = curDirection;
            curDirection = Vector2.down;
            rigid.velocity = curDirection * moveSpeed;
            return;
        }
        curDirection = preDirection;
    }
    public void fall()
    {
        preDirection = curDirection;
        curDirection = Vector2.down;
        rigid.velocity = curDirection * moveSpeed;
        Invoke("ReturnDir", 2f);
    }
    void ReturnDir()
    {
        curDirection = preDirection;
    }
    public void SetJumpForce(float force = 0f)
    {
        if (force == 0f)
        {
            jumpForce = moveSpeed * 2;
            return;
        }
        jumpForce = force;
    }
    private void SetRend(float x)
    {
        if (x < 0) { rend.flipX = false; }
        else { rend.flipX = true; }
    }

    public void CheckReflect(float dir)
    {
        if(dir < 0 && curDirection.x < 0)
        {
            curDirection = right;
        }
        if(dir>0 && curDirection.x>0)
        {
            curDirection = left;
        }
    }
    public void Reflect(Vector3 refdir)
    {
        isCrashing = false;
        rigid.gravityScale = 0f;
        rigid.drag = 0f;
        Vector3 dir = Vector3.Reflect(curDirection, refdir);

        SetDirection(dir.x);
        //curDirection = new Vector3(dir.x, -1, 0f).normalized;
    }    
    public void ReflectFloor(Vector3 refdir)
    {
        isCrashing = true;
        anim.SetBool("isJump", true);   //애니
        
        rigid.velocity = Vector2.zero;
        Vector3 dir = Vector3.Reflect(curDirection, refdir);
        curDirection = new Vector3(dir.x, dir.y, 0f).normalized;
        Debug.Log(refdir);
        if (refdir.y > 0 || refdir.x == 1 || refdir.x == -1)
        {
            //curDirection = new Vector3(dir.x, -1, 0f).normalized;
            SetDirection(dir.x);
        }

        dashCount = 1;  //대쉬 횟수 충전
        rigid.gravityScale = 1f;
        rigid.drag = 1.5f;
        rigid.AddForce(curDirection * jumpForce, ForceMode2D.Impulse);
    }

    

    public void TakeDamage(int dmg)
    {
        hp = hp - dmg < 0 ? 0 : hp - dmg;
        UIManager.instance.SetHp(hp);
        if(hp <= 0)
        {
            Debug.Log("die");
            GameManager.instance.GameOver();
        }
    }

    public void TakeHp(int heal)
    {
        hp = hp + heal > maxHp ? maxHp : hp + heal;
    }
    public void Bomb()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, bombSize);
        foreach(var col in cols)
        {
            if(col.CompareTag("Obstacle"))
            {
                col.SendMessage("Bomb");
            }
        }
    }

    //public void NoDamage()
    //{
    //    gameObject.layer = 9;
    //    Invoke("OnDamage", 1f);
    //}
    //public void OnDamage()
    //{
    //    gameObject.layer = 0;
    //}

    public void Save()
    {
        int score = -(int)transform.position.y;
        SaveManager.instance.Save(score);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, bombSize);
    }
}
