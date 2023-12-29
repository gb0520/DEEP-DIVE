using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceManager : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private GameObject iceObj;
    private List<GameObject> ice = new List<GameObject>();

    int maxIce = 5;

    float timer = 0f;
    [SerializeField]
    float createTime = 5f;

    public Vector2Int minPos;
    private void Awake()
    {
        for(int i = 0; i < maxIce; ++i)
        {
            GameObject newIce = Instantiate(iceObj, transform.position, Quaternion.identity);
            newIce.transform.SetParent(this.transform);
            newIce.SetActive(false);
            ice.Add(newIce);
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer>=createTime)
        {
            timer -= createTime;
            DropIce();
        }
    }

    private void DropIce()
    {
        foreach(GameObject i in ice)
        {
            if(i.activeSelf == false)
            {
                Vector2 pos = new Vector2(Random.Range(minPos.x, minPos.y), target.position.y + 5);
                i.SetActive(true);
                i.SendMessage("SetIce", pos);
                break;
            }
        }
    }
}
