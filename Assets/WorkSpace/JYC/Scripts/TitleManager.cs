using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [Header("Credit")]
    [SerializeField] Transform creditView;
    [SerializeField] Vector2 creditOpenedPos;
    [SerializeField] Vector2 creditClosedPos;
    [SerializeField] GameObject creditCloseBtn;
    [SerializeField] float creditDuration;
    private bool isActing;
    private bool isOpen;
    [Space(20f)]

    [Header("Title")]
    [SerializeField] GameObject titleView;
    [SerializeField] float titleFadeTime;
    [SerializeField] TMP_Text titleTxt;
    [Space(20f)]

    [Header("Start")]
    [SerializeField] GameObject fadeObj;
    [SerializeField] float startTime;


    private TitlePlayer target;
    [SerializeField]
    private Transform monster;

    [SerializeField]
    private float downSpeed = 10f;
    [SerializeField]
    private float maxSpeed = 2f;

    private Tuto tuto;

    [SerializeField]
    private float waitTime = 60f;
    private float timer = 0;
    private bool isEasteEgg = false;
    void Init()
    {
        if(!titleView)
            titleView = GameObject.Find("Title View");

        if(!titleTxt)
            titleTxt = GameObject.Find("How to start").GetComponent<TMP_Text>();

        if(!creditCloseBtn)
            creditCloseBtn = GameObject.Find("CreditOff");

        if(!creditView)
            creditView = GameObject.Find("Credit Image").GetComponent<Transform>();

        if(!fadeObj)
            fadeObj = GameObject.Find("Fade");
    }

    void Awake()
    {
        Time.timeScale = 1f;
        Init();
        SetTitleFade(0.3f);

        target = FindObjectOfType<TitlePlayer>();
        tuto = transform.Find("Tuto").GetComponent<Tuto>();
    }

    private void Update()
    {
        if(titleView.activeSelf == true && isEasteEgg == false)
        {
            timer += Time.deltaTime;
            if(timer >= waitTime)
            {
                timer = 0f;
                isEasteEgg = true;
                target.GoToZem();
            }
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
            creditView.DOLocalMove(creditOpenedPos, creditDuration * 2 / 3).SetEase(Ease.OutBack).OnComplete(() => isActing = false);
        }
        isOpen = !isOpen;
        creditCloseBtn.SetActive(isOpen);
    }

    public void StartBtn()
    {
        if(isEasteEgg == true)
        {
            isEasteEgg = false;
            target.End();
        }
        // 시작한다는 함수 추가?
        Debug.Log("Pressed Start Btn");
        titleView.SetActive(false);
        target.JumpMove();
        // titleView.SetActive(false);
        // GameManager.instance.gameStart = true;
        //fadeObj.SetActive(true);
        //Image fadeImage = fadeObj.GetComponent<Image>();
        //fadeImage.DOFade(1, startTime / 2).OnComplete(() => fadeImage.DOFade(1, startTime / 2).OnComplete(() => MoveScene()));
    }

    public void MobAppear()
    {
        monster.DOLocalMoveY(0, downSpeed).OnComplete(()=>target.Dive());
    }
    public void MobDown()
    {
        if(SaveManager.instance.isFirstTime == true)
        {
            monster.DOLocalMoveY(-15f, maxSpeed).OnComplete(OpenTuto);
            SaveManager.instance.isFirstTime = false;
        }
        else
        {
            MoveScene();
        }
    }
    void OpenTuto()
    {
        tuto.OpenTuto();
    }
    void MoveScene()
    {
        // 씬 이동하는 값 추가
        SceneManager.LoadScene("MainScene");
    }

    public void EndEasteEgg()
    {
        isEasteEgg = false;
    }
}
