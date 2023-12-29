using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float cameraMoveSpeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * cameraMoveSpeed * Time.deltaTime;
    }
    
    public void Save()
    {
        int score = -(int)transform.position.y;
        SaveManager.instance.Save(score);
    }
}
