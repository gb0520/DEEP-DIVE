using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rigid;

    private KeyCode leftKey = KeyCode.A;
    private KeyCode rightKey = KeyCode.D;

    private Vector3 curDirection;
    private Vector3 left = new Vector3(-1, -1, 0).normalized;
    private Vector3 right = new Vector3(1, -1, 0).normalized;


    [SerializeField]
    private Vector2 nowSpeed;

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

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        curDirection = left;
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

        if(isCrashing)
        {
            if (rigid.velocity.y <= 0)
            {
                rigid.gravityScale = 0f;
                rigid.velocity = Vector3.zero;
                curDirection = new Vector3(curDirection.x, -curDirection.y, 0f);
                rigid.drag = 0f;
                //moveSpeed = moveSpeed/2;
                isCrashing = false;
            }
        }
        Move();

        nowSpeed = rigid.velocity;
    }

    private void Move()
    {
        if (isDashing == true || isCrashing == true) { return; }

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
    public void Reflect(Vector3 refdir)
    {
        isCrashing = false;
        rigid.gravityScale = 0f;
        Vector3 dir = Vector3.Reflect(curDirection, refdir);

        curDirection = new Vector3(dir.x, -1, 0f).normalized;
    }
    
    public void ReflectFloor(Vector3 refdir)
    {
        isCrashing = true;
        curDirection = Vector3.Reflect(curDirection, refdir).normalized;

        dashCount = 1;  //대쉬 횟수 충전
        rigid.gravityScale = 1f;
        rigid.drag = 1.5f;
        rigid.AddForce(curDirection * moveSpeed * 2, ForceMode2D.Impulse);
    }
}
