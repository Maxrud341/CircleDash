using UnityEngine;
using System.IO;

[System.Serializable]
public class SoundSettingsData
{
    public float[] soundLevels;
}

public class SaveLoadSettings : MonoBehaviour
{
    public VolumeSettings[] volumeSettings;
    private string filePath;

    void Awake()
    {
        filePath = Path.Combine(Application.persistentDataPath, "soundSettings.json");
        LoadSettings();
    }

    public void SaveSettings()
    {
        SoundSettingsData data = new SoundSettingsData();
        data.soundLevels = new float[volumeSettings.Length];
        
        for (int i = 0; i < volumeSettings.Length; i++)
        {
            data.soundLevels[i] = volumeSettings[i].volumeLvl;
        }
        
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, json);
    }

    public void LoadSettings()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            if (!string.IsNullOrEmpty(json))
            {
                SoundSettingsData data = JsonUtility.FromJson<SoundSettingsData>(json);
                
                for (int i = 0; i < volumeSettings.Length; i++)
                {
                    if (i < data.soundLevels.Length)
                    {
                        volumeSettings[i].volumeLvl = data.soundLevels[i];
                    }
                    else
                    {
                        volumeSettings[i].volumeLvl = 1.0f;
                    }
                    volumeSettings[i].ChangeVolume();
                }
                return;
            }
        }
        
        // Если файла нет или он пуст, создаем его с дефолтными значениями
        SoundSettingsData defaultData = new SoundSettingsData { soundLevels = new float[] { 1.0f, 1.0f } };
        string defaultJson = JsonUtility.ToJson(defaultData, true);
        File.WriteAllText(filePath, defaultJson);
        
        for (int i = 0; i < volumeSettings.Length; i++)
        {
            volumeSettings[i].volumeLvl = 1.0f;
            volumeSettings[i].ChangeVolume();
        }
    }
}
