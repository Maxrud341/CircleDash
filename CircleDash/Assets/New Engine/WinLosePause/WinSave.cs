using UnityEngine;
using System.IO;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[Serializable]
public class WinData
{
    public int sceneNumber;
    public int perfectGradeCount;
    public int goodGradeCount;
    public int missGradeCount;
    public string grade;
    public int score;
    public string dateTime;
}

[Serializable]
public class WinDataCollection
{
    public List<WinData> winDataList = new List<WinData>();
}

public class WinSave : MonoBehaviour
{
    public Win win; // Ссылка на скрипт Win
    private string filePath;

    private void Awake()
    {
        filePath = Path.Combine(Application.persistentDataPath, "winData.json");
    }

    public void SaveWinData()
    {
        WinData newData = new WinData()
        {
            sceneNumber = SceneManager.GetActiveScene().buildIndex,
            perfectGradeCount = win.PerfectGradeCount,
            goodGradeCount = win.GoodGradeCount,
            missGradeCount = win.MissGradeCount,
            grade = win.Grade,
            score = Score.score,
            dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        };

        WinDataCollection dataCollection = LoadWinData();
        dataCollection.winDataList.Add(newData);

        string json = JsonUtility.ToJson(dataCollection, true);
        File.WriteAllText(filePath, json);
        Debug.Log("Статистика добавлена в " + filePath);
    }

    private WinDataCollection LoadWinData()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return JsonUtility.FromJson<WinDataCollection>(json) ?? new WinDataCollection();
        }
        return new WinDataCollection();
    }
}