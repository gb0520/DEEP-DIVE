using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("qkekr�� �浹");
            Vector3 refdir = col.contacts[0].normal;
            col.gameObject.SendMessage("ReflectFloor", refdir);
        }
    }
}
