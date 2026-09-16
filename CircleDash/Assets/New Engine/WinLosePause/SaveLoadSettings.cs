using System.IO;
using UnityEngine;

[System.Serializable]
public class SettingsData
{
    public float musicVolume;
    public float sfxVolume;
    public float latency;
}

public class SaveLoadSettings : MonoBehaviour
{
    public ApplySettings applySettings;
    private string filePath;

    private void Awake()
    {
        filePath = Path.Combine(Application.persistentDataPath, "settings.json");
        LoadSettings();
    }


    public void SaveSettings()
    {
        SettingsData data = new SettingsData
        {
            musicVolume = applySettings.musicVolume,
            sfxVolume = applySettings.sfxVolume,
            latency = applySettings.latency
        };

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, json);
        Debug.Log("Settings saved to: " + filePath);
    }

    public void LoadSettings()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            SettingsData data = JsonUtility.FromJson<SettingsData>(json);

            applySettings.musicVolume = data.musicVolume;
            applySettings.sfxVolume = data.sfxVolume;
            applySettings.latency = data.latency;

            applySettings.ApplyAllSettings();
            Debug.Log("Settings loaded from: " + filePath);
        }
        else
        {
            CreateDefaultSettingsFile();
            applySettings.ApplyAllSettings();
            Debug.Log("Settings file not found. Default settings file created.");
        }
    }

    public void CreateDefaultSettingsFile()
    {
        SettingsData defaultData = new SettingsData
        {
            musicVolume = applySettings.musicVolume,
            sfxVolume = applySettings.sfxVolume,
            latency = applySettings.latency
        };

        string json = JsonUtility.ToJson(defaultData, true);
        File.WriteAllText(filePath, json);
        Debug.Log("Default settings file created at: " + filePath);
    }
}