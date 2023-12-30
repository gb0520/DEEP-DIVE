using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{ 
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            //Debug.Log("벽과 충돌");
            Vector3 refdir = col.contacts[0].normal;
            //col.gameObject.SendMessage("Reflect", refdir);
        }
    }
    
    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Vector3 refdir = col.contacts[0].normal;
            //col.gameObject.SendMessage("CheckReflect", refdir.x);
           // Debug.Log(refdir.x);
        }
    }
}
