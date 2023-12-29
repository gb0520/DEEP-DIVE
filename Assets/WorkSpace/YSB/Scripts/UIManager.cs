using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    private TMP_Text scoreText;
    public Image[] hpImg;


    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }

        scoreText = transform.Find("Text_Score").GetComponent<TMP_Text>();        
    }
    private void Update()
    {
        SetScore();
    }
    public void SetScore()
    {
        scoreText.text = GameManager.instance.score.ToString();
    }

    public void SetHp(int hp)
    {
        foreach(Image img in hpImg)
        {
            img.enabled = false;
        }
        for(int i = 0; i < hp; ++i)
        {
            hpImg[i].enabled = true;
        }
    }
}
