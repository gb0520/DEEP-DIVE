using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuto : MonoBehaviour
{
    private TitleManager manager_Title;
    private GameObject panel;
    [SerializeField]
    private List<GameObject> pages = new List<GameObject>();
    private int pageNum = 0;
    private void Awake()
    {
        manager_Title = GetComponentInParent<TitleManager>();
        panel = transform.GetChild(0).gameObject;
        for(int i = 0; i < pages.Count; ++i)
        {
            pages[i].SetActive(false);
        }
        panel.SetActive(false);
        
    }
    public void OpenTuto()
    {
        panel.SetActive(true);
        pageNum = 0;
        pages[pageNum].SetActive(true);
    }

    public void PrePage()
    {
        pageNum--;
        if (pageNum <= 0)
        {
            pageNum = 0;
        }
        pages[pageNum + 1].SetActive(false);
        pages[pageNum].SetActive(true);
    }
    public void NextPage()
    {
        pageNum++;
        if(pageNum >= pages.Count)
        {
            //¾À ÀüÈ¯
            manager_Title.MobDown();
            return;
        }
        pages[pageNum - 1].SetActive(false);
        pages[pageNum].SetActive(true);
    }
}
