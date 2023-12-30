using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
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
