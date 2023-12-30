using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Linq.Expressions;
using DG.Tweening.Core.Easing;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    private GameObject panel_Pause;
    private TMP_Text scoreText;
    private TMP_Text meterText;
    [SerializeField] GameObject titleView;
    [SerializeField] Transform creditView;
    [SerializeField] Vector2 creditOpenedPos;
    [SerializeField] Vector2 creditClosedPos;
    [SerializeField] GameObject creditCloseBtn;
    [SerializeField] float creditDuration;
    [SerializeField] float titleFadeTime;
    [SerializeField] TMP_Text titleTxt;
    bool isActing;

    private bool isOpen;
    public Image[] hpImg;

    private float fadeTime = 3f;

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
        panel_Pause = transform.Find("Pause").gameObject;
        scoreText = transform.Find("Text_Score").GetComponent<TMP_Text>();      
        meterText = transform.Find("Text_Meter").GetComponent<TMP_Text>();      
        SetTitleFade(0.3f);

        // titleView.SetActive(true);
    }
    private void Update()
    {
        //SetScore();

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            GamePause();
        }
    }
    public void GamePause()
    {
        bool isOpen = !panel_Pause.activeSelf;
        panel_Pause.SetActive(isOpen);
        if (isOpen == true)
        {
            PauseManager.instance.StopTime();
        }
        else
        {
            PauseManager.instance.MoveTime();
        }
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


    void SetTitleFade(float value)
    {
        titleTxt.DOFade(value ,titleFadeTime).OnComplete(()=> SetTitleFade(1 - value));
    }

    

    public void CreditView()
    {
        if(isActing) return;

        isActing = true;
        
        // 열려 있으면 닫힘
        if(isOpen)
        {
            creditView.DOLocalMove(creditClosedPos, creditDuration).SetEase(Ease.InOutBack).OnComplete(() => isActing = false);
        }
        // 닫혀 있으면 열림
        else
        {
            creditView.DOLocalMove(creditOpenedPos, creditDuration).SetEase(Ease.OutBack).OnComplete(() => isActing = false);
        }
        isOpen = !isOpen;
        creditCloseBtn.SetActive(isOpen);
    }

    public void StartBtn()
    {
        // 시작한다는 함수 추가?
        Debug.Log("Pressed Start Btn");
        titleView.SetActive(false);
    }
}
