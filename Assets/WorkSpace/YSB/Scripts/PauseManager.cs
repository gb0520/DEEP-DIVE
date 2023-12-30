using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager instance;
    public float stopTime;
    bool isPaused = false;

    public bool IsPaused { set { isPaused = value; } }

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
        if(isPaused == true) { return; }
        Time.timeScale = 1;
    }
}
