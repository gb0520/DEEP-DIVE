using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Xml;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    public int highestScore;
    public bool isFirstTime;

    void Awake()
    {
        instance = this;
        Load();
    }


    public void Save(int score)
    {
        if (score <= highestScore) return;

        highestScore = score;

        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.AppendChild(xmlDocument.CreateXmlDeclaration("1.0", "utf-8", "yes"));

        XmlElement root = xmlDocument.CreateElement("Save");
        root.SetAttribute("FileName", "Dive_Game_Data");

        XmlElement highestScoreData = xmlDocument.CreateElement("HighestScore");
        highestScoreData.InnerText = score.ToString();
        root.AppendChild(highestScoreData);

        XmlElement firstTime = xmlDocument.CreateElement("FirstTime");
        firstTime.InnerText = isFirstTime.ToString();
        root.AppendChild(firstTime);

        xmlDocument.AppendChild(root);

        xmlDocument.Save(Application.dataPath + "/HighScore.xml");
        if(File.Exists(Application.dataPath + "/HighScore.xml"))
        {
            Debug.Log("Saved");
        }
    }

    public void Load()
    {
        if(File.Exists(Application.dataPath + "/HighScore.xml"))
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(Application.dataPath + "/HighScore.xml");

            XmlNodeList score = xmlDocument.GetElementsByTagName("HighestScore");
            int Score = int.Parse(score[0].InnerText);
            highestScore = Score;

            XmlNodeList firstTime = xmlDocument.GetElementsByTagName("FirstTime");
            bool FirstTime = bool.Parse(firstTime[0].InnerText);
            isFirstTime = FirstTime;
            
        }
        else
        {
            ResetData();
            Debug.Log("no file");
        }


    }

    
    public void ResetData()
    {
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.AppendChild(xmlDocument.CreateXmlDeclaration("1.0", "utf-8", "yes"));

        
        XmlElement root = xmlDocument.CreateElement("Save");
        root.SetAttribute("FileName", "Dive_Game_Data");

        XmlElement score = xmlDocument.CreateElement("HighestScore");
        score.InnerText = 0.ToString();
        root.AppendChild(score);

        XmlElement firstTime = xmlDocument.CreateElement("FirstTime");
        firstTime.InnerText = true.ToString();
        root.AppendChild(firstTime);

       
        xmlDocument.AppendChild(root);

        xmlDocument.Save(Application.dataPath + "/HighScore.xml");
        if(File.Exists(Application.dataPath + "/HighScore.xml"))
        {
            Debug.Log("Saved");
            // 저장 성공
        }

    }

    // public void AutoSave()
    // {
    //     Save();
    //     Invoke("AutoSave", 30f);
    // }
}
