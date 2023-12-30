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
                monster.DOKill();
                monster.DOLocalMoveY(3, monsterInsideTime).SetEase(Ease.OutQuart);
                isComing = true;
            }
            return;    
        }

        
        if(isComing)
        {
            isComing = false;
            monster.DOKill();
            monster.DOLocalMoveY(4, monsterInsideTime).SetEase(Ease.InExpo);
        }

        transform.position = new Vector3(0, player.YPos, transform.position.z);
    }


    
}
