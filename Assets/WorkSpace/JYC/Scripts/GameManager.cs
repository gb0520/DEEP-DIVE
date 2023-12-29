using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SocialPlatforms.Impl;
using System;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] GameObject fadeObj;

    [SerializeField] ObstacleManager obstacleManager;
    [SerializeField] Transform cameraTrans;

    [SerializeField] float waitTime;
    [SerializeField] float fadeTime;

    [SerializeField] PlayerMove player;
    public float targetY;
    public float recentY;
    private float errorFix;

    [SerializeField] private float fadeOutFix;
    [SerializeField] private float startY;
    [SerializeField] private float endY;


    [SerializeField] float length;
    public bool isOverY;
    public bool isClearStage;
    private int cnt;

    public int stage;

    public float score => Mathf.Abs(player.YPos) - Mathf.Abs(fadeOutFix);
    private float screenY => Camera.main.orthographicSize * 2;

    void Awake()
    {
        if(!instance)
            instance = this;

        fadeOutFix = 0f;
        recentY = player.YPos;
        targetY = recentY - length;
        stage = cnt = 0;
    }

    void Update()
    {
        if(isClearStage) return;

        if (targetY < cameraTrans.position.y - 2 * screenY) return;
        
        if(!isOverY)
        {
            errorFix = targetY - obstacleManager.lowestY;
            targetY = obstacleManager.lowestY;

            isOverY = true;
            return;
        }
            
        if(!isClearStage)
        {
            isClearStage = true;
            fadeObj.GetComponent<SpriteRenderer>().DOFade(1, fadeTime);
            startY = player.YPos;
            
            player.SetDirection(isClearStage);  //플레이어 낙하 방향 조정
            Invoke("FadeOut", waitTime - fadeTime);
            Invoke("Restart", waitTime);
        }
    }

    void FadeIn()
    {
        fadeObj.GetComponent<SpriteRenderer>().DOFade(1, fadeTime).OnComplete(ClearStage);
    }

    void ClearStage()
    {
        if (stage < 2)
            stage ++;
        cnt ++;
    }

    // 페이드인, 페이드아웃
    void FadeOut()
    {
        
        fadeObj.GetComponent<SpriteRenderer>().DOFade(0, fadeTime);
    }

    void Restart()
    {
        endY = player.YPos;
        fadeOutFix += startY - endY;
        obstacleManager.targetPos = new Vector3(0, player.YPos - screenY / 2, 0);
        recentY = player.YPos;
        
        targetY = recentY - length + errorFix;

        isClearStage = false;
        isOverY = false;
        player.SetDirection(isClearStage);  //플레이어 낙하 방향 조정
    }

    public void GameOver()
    {
        SaveManager.instance.Save((int)score);
        Time.timeScale = 0f;
    }
}
