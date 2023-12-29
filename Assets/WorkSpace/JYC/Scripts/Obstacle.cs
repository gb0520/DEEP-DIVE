using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public Transform highest;
    public Transform lowest;


    public void Spawn(Vector3 pos)
    {
        // 소환 할 때 위치 보정하기
        // 전달 받는 pos는 highest의 위치를 의미함
        Vector3 spawnPos = pos - new Vector3(0, highest.localPosition.y, 0) * transform.localScale.y;
        transform.position = spawnPos;
    }




}
