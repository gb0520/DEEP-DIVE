using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollManager : MonoBehaviour
{
    public Transform[] scrollObjs;
    public Transform cameraTrans;

    public int highestIndex;
    public int lowestIndex;

    void Start()
    {
        highestIndex = 0;
        lowestIndex = scrollObjs.Length - 1;
        float screenY = Camera.main.orthographicSize * 2;

        for (int i = 0; i < scrollObjs.Length; i++)
        {
            // float objY = cameraTrans.position.y + screenY + -i * scrollObjs[i].localScale.y;
            float objY = screenY + -i * scrollObjs[i].localScale.y;
            scrollObjs[i].position = new Vector3(0, objY, 0);
        }
    }

    void Update()
    {
        Scrolling();
    }


    void Scrolling()
    {
        float screenY = Camera.main.orthographicSize * 2;
        // Vector3 highestPos = cameraTrans.position + Vector3.up * screenY;
        float highestY = cameraTrans.position.y + screenY;

        // 가장 높이 있는 인덱스 값만 조사하는 방법
        if(scrollObjs[highestIndex].position.y > highestY)
        {
            // float lowestY = cameraTrans.position.y - screenY;
            // 이거 값을 가장 낮게 있는 값으로 조절하면 적당하게 할 수 있을 것 같음
            // 결국 Y값을 이렇게 하면 오차가 생길 수 밖에 없을 것 같음
            // scrollObjs[highestIndex].position = new Vector3(0, lowestY, 0);

            // 그래서 lowestIndex를 만들어서 이 오브젝트의 Y값에서 오브젝트의 스케일 값 만큼 낮게 이동하는 것으로 변경
            // 이거의 근본적인 문제점은 이미지가 1인 크기보다 더 큰 이미지가 왔을 때 전혀 대응할 수 없어 보인다.
            // 일단 테스트
            // scrollObjs[highestIndex].position = scrollObjs[lowestIndex].position + Vector3.down * scrollObjs[highestIndex].localScale.y;
            scrollObjs[highestIndex].position = scrollObjs[lowestIndex].position + Vector3.down * 10;
            

            highestIndex ++;
            if(highestIndex == scrollObjs.Length) highestIndex = 0;

            lowestIndex ++;
            if (lowestIndex == scrollObjs.Length) lowestIndex = 0;
        }

        // 모두 체크해서 하나하나 확인하는 방법
        // for (int i = 0; i < scrollObjs.Length; i++)
        // {
        //     if (scrollObjs[i].position.y > highestPos.y)
        //     {
        //         float lowestY = cameraTrans.position.y - screenY;
        //         scrollObjs[i].position = new Vector3(0, lowestY, 0);
        //         // cameraTrans.position - Vector3.up * screenY;
        //     }
                
        // }
    }


}
