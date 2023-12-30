using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour
{
    Rigidbody2D rigid;
    [SerializeField]
    float moveSpeed;

    float limitY = 0;

    public void OnTriggerTouch()
    {
        rigid.velocity = Vector2.down * moveSpeed;
    }

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        limitY = transform.position.y - 20f;
    }
    private void Update()
    {
        if (transform.position.y < limitY)
        {
            gameObject.SetActive(false);
        }
    }
}
