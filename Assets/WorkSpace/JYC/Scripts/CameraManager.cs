using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;
using DG.Tweening;

public class CameraManager : MonoBehaviour
{
    [SerializeField] PlayerMove player;
    [SerializeField] Transform monster;
    [SerializeField] float monsterInsideTime;

    bool isComing;
    // bool isOutsiding = player;

    void Awake()
    {
        if(!monster)
            monster = GameObject.Find("Monster_0").GetComponent<Transform>();
    }
    
    void Update()
    {
        Move();
    }

    public void Move()
    {


        if(transform.position.y < player.YPos) 
        {
            if(!isComing)
            {
                isComing = true;
                monster.DOKill();
                monster.DOLocalMoveY(-4, monsterInsideTime * 4);
                // monster.DOLocalMoveY(4, 0.2f).OnComplete(()=>monster.DOLocalMoveY(-4, monsterInsideTime * 4));
                
            }
            return;    
        }

        
        if(isComing)
        {
            isComing = false;
            monster.DOKill();
            monster.DOLocalMoveY(4, monsterInsideTime * 2);
        }

        transform.position = new Vector3(0, player.YPos, transform.position.z);
    }


    
}
