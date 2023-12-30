using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    private TMP_Text scoreText;
    private TMP_Text meterText;
    public Image[] hpImg;

    private float fadeTime = 3f;

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
        scoreText = transform.Find("Text_Score").GetComponent<TMP_Text>();      
        meterText = transform.Find("Text_Meter").GetComponent<TMP_Text>();        
    }
    private void Update()
    {
        //SetScore();
    }
    public void SetScore(float score)
    {
        scoreText.text = GameManager.instance.score.ToString("F2") + "M";
    }
    public void ShowMeter(float meter)
    {
        if(meter <= 0)
        {
            meterText.color = new Color(1, 1, 1, 1);
            meterText.gameObject.SetActive(false);
            return;
        }
        meterText.gameObject.SetActive(true);
        int m = (int)(meter / 100) * 100;
        meterText.text = m.ToString("F0") + "M\n����";

        DOTmp(meterText, 2f);

        Invoke("OutMeter", 3f);
    }
    void OutMeter()
    {
        meterText.DOFade(0, fadeTime);
    }
    public void DOTmp(TMP_Text text, float duration)
    {
        text.maxVisibleCharacters = 0;
        DOTween.To(x => text.maxVisibleCharacters = (int)x, 0f, text.text.Length, duration);
    }
    public void SetHp(int hp)
    {
        for (int i = 0; i < hpImg.Length; ++i)
        {
            hpImg[i].enabled = false;
        }
        for(int i = 0; i < hp; ++i)
        {
            hpImg[i].enabled = true;
        }
    }
}