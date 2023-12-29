using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SocialPlatforms.Impl;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject fadeObj;

    public ObstacleManager obstacleManager;
    public Transform cameraTrans;

    public float waitTime;
    public float fadeTime;

    public PlayerMove player;
    public float targetY;
    public float recentY;

    [SerializeField] float length;
    public bool isOverY;
    public bool isClearStage;
    private float score;
    private float screenY => Camera.main.orthographicSize * 2;

    void Awake()
    {
        if(!instance)
            instance = this;

        
        recentY = player.YPos;
        targetY = recentY - length;
    }

    void Update()
    {
        if(isClearStage) return;

        if (targetY < cameraTrans.position.y - 2 * screenY) return;
        
        if(!isOverY)
        {
            targetY = obstacleManager.lowestY;
            isOverY = true;
            return;
        }
            
        if(!isClearStage)
        {
            score += recentY - targetY;
            isClearStage = true;
            fadeObj.GetComponent<SpriteRenderer>().DOFade(1, fadeTime);
            player.SetDirection(isClearStage);  //플레이어 낙하 방향 조정
            Invoke("FadeOut", waitTime - fadeTime);
            Invoke("Restart", waitTime);
        }
    }

    // 페이드인, 페이드아웃
    void FadeOut()
    {
        fadeObj.GetComponent<SpriteRenderer>().DOFade(0, fadeTime);
    }

    void Restart()
    {
        obstacleManager.targetPos = new Vector3(0, player.YPos - screenY / 2, 0);
        recentY = player.YPos;
        targetY = recentY - length;

        isClearStage = false;
        isOverY = false;
        player.SetDirection(isClearStage);  //플레이어 낙하 방향 조정
    }

    public void GameOver()
    {
        score += recentY - player.YPos;
        SaveManager.instance.Save((int)score);
        Time.timeScale = 0f;
    }
}
