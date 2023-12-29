using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ScrollManager : MonoBehaviour
{
    public Transform[] scrollObjs;
    [SerializeField] Transform cameraTrans;

    [SerializeField] private float verSize;
    [SerializeField] private Sprite[] wallSprite;
    [SerializeField] private SpriteRenderer[] scrollRenderers;
    

    private int highestIndex;
    private int lowestIndex;

    void Start()
    {
        // highestIndex = 0;
        // lowestIndex = scrollObjs.Length - 1;
        // float screenY = Camera.main.orthographicSize * 2;

        // for (int i = 0; i < scrollObjs.Length; i++)
        // {
        //     // float objY = cameraTrans.position.y + screenY + -i * scrollObjs[i].localScale.y;
        //     float objY = screenY + -i * scrollObjs[i].localScale.y;
        //     scrollObjs[i].position = new Vector3(0, objY, 0);
        // }
    }

    void Awake() 
    {
        highestIndex = 0;
        lowestIndex = scrollObjs.Length - 1;
        float screenY = Camera.main.orthographicSize * 2;

        for (int i = 0; i < scrollObjs.Length; i++)
        {
            // float objY = cameraTrans.position.y + screenY + -i * scrollObjs[i].localScale.y;
            float objY = screenY + -i * verSize;
            scrollObjs[i].position = new Vector3(0, objY, 0);
            
        }    
    }

    void Update()
    {
        Scrolling();
        Check();
    }

    void Check()
    {
        if(scrollRenderers[0].sprite != wallSprite[GameManager.instance.stage])
        {
            for (int i = 0; i < scrollRenderers.Length; i++)
            {
                scrollRenderers[i].sprite = wallSprite[GameManager.instance.stage];
            }
        }
    }

    

    // 가장 높이 있는 오브젝트의 Y값이 임계값에 다다르면 가장 낮게 있는 오브젝트의 아래로 소환함
    void Scrolling()
    {
        float screenY = Camera.main.orthographicSize * 2;
        float highestY = cameraTrans.position.y + screenY;

        if(scrollObjs[highestIndex].position.y > highestY)
        {
            scrollObjs[highestIndex].position = scrollObjs[lowestIndex].position + Vector3.down * verSize;
            

            highestIndex ++;
            if(highestIndex == scrollObjs.Length) highestIndex = 0;

            lowestIndex ++;
            if (lowestIndex == scrollObjs.Length) lowestIndex = 0;
        }
    }


}
