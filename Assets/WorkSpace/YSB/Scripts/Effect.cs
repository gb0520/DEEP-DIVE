using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void SetPos(Vector2 pos)
    {
        transform.position = pos;
    }

    private void Update()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f)
        {
            gameObject.SetActive(false);
        }
    }
}
