using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.SendMessage("PickUp");
        gameObject.SetActive(false);
    }
}
