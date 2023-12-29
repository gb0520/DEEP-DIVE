using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] float cameraMoveSpeed;
    [SerializeField] PlayerMove player;
    float playerYPos;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        // transform.position += Vector3.down * cameraMoveSpeed * Time.deltaTime;
    }

    public void Move()
    {
        if(transform.position.y < player.YPos) return;

        transform.position = new Vector3(0, player.YPos, transform.position.z);
    }


    
}
