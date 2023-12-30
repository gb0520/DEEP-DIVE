using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCrash : MonoBehaviour
{
    private PlayerMove player;
    public Vector2Int dir;

    private void Awake()
    {
        player = transform.GetComponentInParent<PlayerMove>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("cndehf");
        player.Reflect(dir.x, dir.y);
    }
}
