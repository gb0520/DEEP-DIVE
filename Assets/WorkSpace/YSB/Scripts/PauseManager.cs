using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager instance;
    public float stopTime;

    private void Awake()
    {
        if (instance == null) instance = this;
    }
    
    public void TakeDamageTime()
    {
        StopCoroutine(TimeMove());
        StopTime();
        StartCoroutine(TimeMove());
    }
    private IEnumerator TimeMove()
    {
        yield return new WaitForSecondsRealtime(stopTime);
        MoveTime();
    }
    public void StopTime()
    {
        Time.timeScale = 0;
    }
    public void MoveTime()
    {
        Time.timeScale = 1;
    }
}
