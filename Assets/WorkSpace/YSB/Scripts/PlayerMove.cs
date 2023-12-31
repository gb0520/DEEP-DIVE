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

    private Vector3 preDirection;   //���� ���� > ����
    private Vector3 curDirection;
    private Vector3 left = new Vector3(-1, -1, 0).normalized;
    private Vector3 right = new Vector3(1, -1, 0).normalized;

    public float YPos => transform.position.y;

    [SerializeField]
    private PlayerDashEft playerDashEft;

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
    private int dashCount =  2;

    private bool isCrashing = false;
    private bool isLoading = false;
    private bool isHit = false;
    private float jumpForce;

    public bool IsDashing => isDashing;

    [SerializeField]
    private float bombSize;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        rend = GetComponentInChildren<SpriteRenderer>();
        curDirection = left;
        preDirection = curDirection;

        jumpForce = moveSpeed * 1.5f;
        hp = maxHp;

        //StartCoroutine(ChargeDashCount());
    }
    private void Update()
    {
        if (GameManager.instance.isGameOver == true) { return; }
        if (Input.GetKeyDown(leftKey))
        {
            StartDash(left);
        }
        if (Input.GetKeyDown(rightKey))
        {
            StartDash(right);
        }

        SetRend(curDirection.x);

        if(isCrashing == false && rigid.velocity.y > 0)
        {
            if (isLoading == true) { return; }
            float x = curDirection.x < 0 ? -1 : 1;
            curDirection = new Vector2(x, -1).normalized;
        }
        //if(rigid.velocity.y > 0)
        //{
        //    if(isLoading == true) { return; }
        //    curDirection = new Vector3(curDirection.x, -curDirection.y, 0f);
        //}

        if(isCrashing)
        {
            if (rigid.velocity.y <= 0.1f)
            {
                rigid.velocity = Vector3.zero;
                rigid.gravityScale = 0f;
                rigid.drag = 0f;
                anim.SetBool("isJump", false);

                curDirection = new Vector3(curDirection.x, -curDirection.y, 0f);
                isCrashing = false;
            }
        }
        Move();        

        nowSpeed = rigid.velocity;
    }

    //----------------------------------------------Move

    public void OnInputLeft()
    {
        StartDash(left);
    }
    public void OnInputRight()
    {
        StartDash(right);
    }

    public void Accel()
    {
        maxSpeed *= 1.25f;
    }
    private void Move()
    {
        if (isDashing == true || isCrashing == true || isLoading == true) { return; }
        if(moveSpeed < maxSpeed)
        {
            moveSpeed += Time.deltaTime;
            if (moveSpeed >= maxSpeed)
            {
                moveSpeed = maxSpeed;
            }
        }
        if (rigid.velocity.y > 0) return;
        rigid.velocity = curDirection * moveSpeed;
    }

    void ChargeDash()
    {
        dashCount = 1;
        playerDashEft.Play_Charge();
    }

    private void StartDash(Vector3 dir)
    {
        if (isDashing || isLoading == true) { return; }
        isDashing = true;
        isCrashing = false;
        //dashCount -= 1;
        curDirection = dir;
        rigid.velocity = curDirection * dashSpeed;
        StartCoroutine(Dash());

        playerDashEft.Stop_Charge();
        playerDashEft.Play_Use();
        SoundManager.instance.PlaySfx(SoundManager.Sfx.dash);
    }

    private IEnumerator ChargeDashCount()
    {
        float timer = 0f;
        while (true)
        {
            timer += Time.deltaTime;
            if (timer >= 2f)
            {
                timer -= 2f;
                if(dashCount < 2)
                {
                    dashCount++;
                }
            }
            yield return null;
        }
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
    public void SetDirection(float x)  //x������ ����
    {
        anim.SetBool("isJump", false);
        if (x < 0)
        {
            curDirection = left;
        }
        else if(x > 0)
        {
            curDirection = right;
        }
    }
    public void SetDirection(bool loading)  //��ü ����
    {
        isLoading = loading;
        if(isLoading == true)
        {
            if(curDirection != Vector3.down) { preDirection = curDirection; }
            isDashing = false;
            curDirection = Vector2.down;
            rigid.velocity = curDirection * moveSpeed;
            return;
        }
        curDirection = preDirection;
    }
    public void fall()
    {
        if (curDirection != Vector3.down) { preDirection = curDirection; }
        Debug.Log("�浹");
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
            jumpForce = moveSpeed * 1.5f;
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
        //isDashing = false;
        //isCrashing = false;
        //rigid.gravityScale = 0f;
        //rigid.drag = 0f;
        //Vector3 dir = Vector3.Reflect(curDirection, refdir);
        //ChargeDash();
        //SetDirection(dir.x);
        ////curDirection = new Vector3(dir.x, -1, 0f).normalized;
    }    
    public void ReflectFloor(Vector3 refdir)
    {
        //isCrashing = true;
        //anim.SetBool("isJump", true);   //�ִ�
        
        //rigid.velocity = Vector2.zero;
        ////Vector3 dir = Vector3.Reflect(curDirection, refdir);
        ////curDirection = new Vector3(dir.x, dir.y, 0f).normalized;
        //Debug.Log(refdir);
        ////if (refdir.y > 0 || refdir.x != 0)//refdir.x == 1 || refdir.x == -1)
        ////{
        ////    //curDirection = new Vector3(dir.x, -1, 0f).normalized;
        ////    SetDirection(dir.x);
        ////}
        //float x;
        //float y;
        //if (refdir.y < -0.5f)    //���� ƨ����
        //{
        //    x = curDirection.x < 0 ? -1 : 1;
        //    y = 1;
        //}
        //else if(refdir.y > 0.5f)//�Ʒ��� ƨ��
        //{
        //    x = curDirection.x;
        //    y = -1;
        //}
        //else  //����
        //{
        //    x = -curDirection.x;
        //    y = -1;
        //}
        //curDirection = new Vector2(x, y).normalized;

        

        //ChargeDash();
        //rigid.gravityScale = 1f;
        //rigid.drag = 1.5f;
        //rigid.AddForce(curDirection * jumpForce, ForceMode2D.Impulse);
    }

    

    public void TakeDamage(float dmg)
    {
        if (isHit == true || GameManager.instance.isGameOver) { return; }
        //hp = hp - dmg < 0 ? 0 : hp - dmg;
        Invoke("NoDamage", 0.4f);
        //NoDamage();
        isHit = true;
        anim.SetTrigger("isHit");
        EffectManager.instance.DrawEffect();
        //UIManager.instance.SetHp(hp);
        GameManager.instance.MinusTime(dmg);
        PauseManager.instance.TakeDamageTime();
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

    public void NoDamage()
    {
        isHit = false;
        //transform.GetChild(0).gameObject.layer = 9;
        //Invoke("OnDamage", 0.4f);
    }
    public void OnDamage()
    {
        transform.GetChild(0).gameObject.layer = 0;
    }

    public void Save()
    {
        int score = -(int)transform.position.y;
        SaveManager.instance.Save(score);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, bombSize);
    }

    public void Reflect(float xdir, float ydir)
    {
        moveSpeed = maxSpeed;
        rigid.velocity = Vector2.zero;

        isDashing = false;

        float x = xdir != 0 ? xdir : curDirection.x < 0 ? -1 : 1;
        float y = ydir;

        curDirection = new Vector2(x, y).normalized;

        if(y > 0)
        {
            //isCrashing = true;
            rigid.gravityScale = 1f;
            rigid.drag = 1.5f;
            rigid.AddForce(curDirection * jumpForce, ForceMode2D.Impulse);
        }
        else
        {
            rigid.gravityScale = 0f;
            rigid.drag = 0f;
            rigid.velocity = curDirection * moveSpeed;
        }
        ChargeDash();
    }
}
