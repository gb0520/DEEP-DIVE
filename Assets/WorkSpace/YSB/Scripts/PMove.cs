using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMove : MonoBehaviour
{
    private KeyCode leftKey = KeyCode.A;
    private KeyCode rightKey = KeyCode.D;

    [SerializeField]
    private float moveSpeed = 0f;
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float dashSpeed;
    [SerializeField]
    private float dashTime;
    private bool isDashing = false;
    private int dashCount = 1;

    [SerializeField]
    private bool isCrashing = false;
    private bool isWallCrashing = false;

    [SerializeField]
    private Vector3 curDirection;
    private Vector3 left = new Vector3(-1, -1, 0).normalized;
    private Vector3 right = new Vector3(1, -1, 0).normalized;

    float time = 0f;
    private float rad = 45f;
    [SerializeField]
    private float gravity = 9.81f;

    private Vector3 crashPoint;
    private Vector3 wallCrashPoint;
    private void Start()
    {
        curDirection = left;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(leftKey))
        {
            StartDash(left);
        }
        if (Input.GetKeyDown(rightKey))
        {
            StartDash(right);
        }

        CheckCrash();
        Crash();
        Move();
    }

    private void CheckCrash()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.5f, 1 << LayerMask.NameToLayer("Floor"));
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position, Vector2.left, 0.5f, 1 << LayerMask.NameToLayer("Wall"));
        RaycastHit2D hit3 = Physics2D.Raycast(transform.position, Vector2.right, 0.5f, 1 << LayerMask.NameToLayer("Wall"));

        if (hit2.collider != null || hit3.collider != null)
        {
            if (wallCrashPoint != transform.position) { isWallCrashing = false; }
            if (isWallCrashing == false)
            {
                isCrashing = false;
                curDirection = new Vector3(-curDirection.x, curDirection.y, 0f);
                wallCrashPoint = transform.position;
                isWallCrashing = true;
            }
        }
        //¹Ù´Ú Ãæµ¹
        if (hit.collider != null)
        {
            if(isCrashing == false)
            {
                Debug.Log("¶¥ Ãæµ¹");
                crashPoint = transform.position;
                isCrashing = true;
                isWallCrashing = false;
            }

        }        
    }

    private void Move()
    {
        if (isDashing == true || isCrashing == true) { return; }

        moveSpeed += Time.deltaTime;
        if (moveSpeed >= maxSpeed)
        {
            moveSpeed = maxSpeed;
        }
        transform.position += curDirection * moveSpeed * Time.deltaTime;
    }

    private void StartDash(Vector3 dir)
    {
        if (dashCount <= 0 || isCrashing == true) { return; }
        isDashing = true;
        dashCount -= 1;
        curDirection = dir;
        StartCoroutine(Dash());
        
    }

    private IEnumerator Dash()
    {
        float timer = 0f;
        while(true)
        {
            timer += Time.deltaTime;
            if(timer >= dashTime)
            {
                isDashing = false;
                yield break;
            }
            transform.position += curDirection * dashSpeed * Time.deltaTime;
            yield return null;
        }
    }
    public void Crash()
    {
        if(isCrashing == false) { return; }
        time += Time.deltaTime;

        float vx = 0;
        if(curDirection.x < 0)
        {
            vx =  -1f * Mathf.Cos(rad * Mathf.Deg2Rad) * moveSpeed * time;
        }
        else
        {
            vx = 1f * Mathf.Cos(rad * Mathf.Deg2Rad) * moveSpeed * time;
        }
        float vy = Mathf.Sin(rad * Mathf.Deg2Rad) * moveSpeed * time;

        double y = vy - (0.5 * gravity * Mathf.Pow(time, 2));

        transform.position = new Vector2(vx + crashPoint.x, (float)y + crashPoint.y);

        if(transform.position.y >= 1.5f + crashPoint.y)
        {
            isCrashing = false;
            dashCount = 1;
        }
    }
    public void Reflect(Vector3 refdir)
    {
        curDirection = Vector3.Reflect(curDirection, refdir).normalized;
    }
}
