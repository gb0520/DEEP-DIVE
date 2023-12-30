using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;
using DG.Tweening;

public class CameraManager : MonoBehaviour
{
    [SerializeField] PlayerMove player;
    [SerializeField] Transform monster;
    [SerializeField] Transform monsterParent;

    [SerializeField] float monsterInsideTime;

    bool isComing;
    bool isOutComing;

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
        if(player.IsDashing && !isOutComing)
        {
            isOutComing = true;
            monsterParent.DOKill();
            monsterParent.DOLocalMoveY(1f, 0.5f).OnComplete(() => isOutComing = false);
        }
        else if(!player.IsDashing && !isOutComing)
        {
            monsterParent.DOKill();
            monsterParent.DOLocalMoveY(0, 2f);
        }



        if(transform.position.y < player.YPos) 
        {
            if(!isComing)
            {
                isComing = true;
                monster.DOKill();
                // isOutComing = false;
                monster.DOLocalMoveY(-4, monsterInsideTime * 4);
                // monster.DOLocalMoveY(4, 0.2f).OnComplete(()=>monster.DOLocalMoveY(-4, monsterInsideTime * 4));
                
            }
            return;    
        }

        
        if(isComing)
        {
            isComing = false;
            // if(!isOutComing)
            monster.DOKill();
            // isOutComing = false;
            monster.DOLocalMoveY(4, monsterInsideTime * 2);
        }

        transform.position = new Vector3(0, player.YPos, transform.position.z);
    }


    
}
