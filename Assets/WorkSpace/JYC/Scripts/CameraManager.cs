using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] PlayerMove player;
    

    void Start()
    {
        
    }

    
    void Update()
    {
        Move();
    }

    public void Move()
    {
        if(transform.position.y < player.YPos) return;

        transform.position = new Vector3(0, player.YPos, transform.position.z);
    }


    
}
