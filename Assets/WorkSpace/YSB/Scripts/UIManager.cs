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
    private bool isOpen;
    public Image[] hpImg;

    [SerializeField] GameObject overPanel;
    [SerializeField] TMP_Text bestScoreText;
    [SerializeField] TMP_Text recentScoreText;
    [SerializeField] TMP_Text gameOverText;
    [SerializeField] string[] gameOverString;
    [SerializeField] GameObject restartBtn;
    [SerializeField] ZB.UsingTweening.Guage.TweeningGuage hpGuage;
  

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
        
        Init();
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

    void Init()
    {   
        if(!overPanel)
            overPanel = GameObject.Find("GameOver Panel");

        if(!bestScoreText)
            bestScoreText = GameObject.Find("HighestScore Text").GetComponent<TMP_Text>();
            
        if(!recentScoreText)
            recentScoreText = GameObject.Find("RecentScore Text").GetComponent<TMP_Text>();

        if(!gameOverText)
            gameOverText = GameObject.Find("GameOver Text").GetComponent<TMP_Text>();
            
        if(!restartBtn)
            restartBtn = GameObject.Find("Restart Btn");
    }


    public void GamePause()
    {
        if(GameManager.instance.isGameOver == true) { return; }
        bool isOpen = !panel_Pause.activeSelf;
        panel_Pause.SetActive(isOpen);
        if (isOpen == true)
        {
            PauseManager.instance.IsPaused = true;
            PauseManager.instance.StopTime();

        }
        else
        {
            PauseManager.instance.IsPaused = false;
            PauseManager.instance.MoveTime();

        }
    }
    public void SetScore(float score)
    {
        scoreText.text = GameManager.instance.score.ToString("F2") + "M";
    }
    public void ShowMeter(int stage)
    {
        // if(stage <= 0)
        // {
        //     meterText.color = new Color(1, 1, 1, 1);
        //     meterText.gameObject.SetActive(false);
        //     return;
        // }
        
        meterText.color = new Color(1, 1, 1, 1);
        meterText.gameObject.SetActive(true);
        meterText.text = (stage + 1).ToString() + " 스테이지" + "\n돌파";

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
    public void SetGuage(float ratio)
    {
        hpGuage.Change(ratio);
    }

    public void GameOver()
    {
        overPanel.SetActive(true);
        gameOverText.text = "";
        bestScoreText.text = "";
        recentScoreText.text = "";
        overPanel.GetComponent<Image>().DOFade(1, 2f).OnComplete(() => GameOverUI());
    }
    
    public void GameOverUI()
    {
        gameOverText.text = gameOverString[Random.Range(0, gameOverString.Length)];
        bestScoreText.maxVisibleCharacters = 0;
        string bestScore = "최고 점수 : " + SaveManager.instance.highestScore.ToString() + "M";
        bestScoreText.text = bestScore;
        DOTween.To(x => bestScoreText.maxVisibleCharacters = (int)x, 0f, bestScoreText.text.Length, 1f);

        recentScoreText.maxVisibleCharacters = 0;
        string recentScore = "최근 점수 : " + ((int)GameManager.instance.score).ToString() + "M";
        recentScoreText.text = recentScore;
        DOTween.To(x => recentScoreText.maxVisibleCharacters = (int)x, 0f, recentScoreText.text.Length, 1f).OnComplete(() => restartBtn.SetActive(true));
    }

    

    
}
