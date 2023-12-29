using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public Transform highest;
    public Transform lowest;
    public GameObject[] obstacles;
    public int index;

    public void Spawn(Vector3 pos)
    {
        // 소환 할 때 위치 보정하기
        // 전달 받는 pos는 highest의 위치를 의미함
        Vector3 spawnPos = pos - new Vector3(0, highest.localPosition.y, 0) * transform.localScale.y;
        transform.position = spawnPos;
    }


    public void Init()
    {
        for (int i = 0; i < obstacles.Length; i++)
        {
            if (!obstacles[i].activeSelf)
                obstacles[i].SetActive(true);
        }
    }
    // 장애물 초기화 기능 추가해야 함


}
