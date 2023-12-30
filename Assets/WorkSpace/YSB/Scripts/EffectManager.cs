using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager instance;
    [SerializeField]
    private Transform target;

    [SerializeField]
    private GameObject effectObj;
    private List<GameObject> effects = new List<GameObject>();

    int maxCount = 10;

    private void Awake()
    {
        if (instance == null) { instance = this; }

        target = FindObjectOfType<PlayerMove>().transform;
        for (int i = 0; i < maxCount; ++i)
        {
            GameObject newEffect = Instantiate(effectObj, transform.position, Quaternion.identity);
            newEffect.transform.SetParent(this.transform);
            newEffect.SetActive(false);
            effects.Add(newEffect);
        }
    }

    public void DrawEffect()
    {
        foreach (GameObject i in effects)
        {
            if (i.activeSelf == false)
            {
                Vector2 pos = target.position;
                i.SetActive(true);
                i.SendMessage("SetPos", pos);
                break;
            }
        }
    }
}
