using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Networking;
using TMPro;

public class CustomMapManager : MonoBehaviour
{
    public RhythmEngine rhythmEngine;
    public static AudioClip LoadedClip;
    public static string LoadedClipName;
    public Button playButton;
    public GameObject loadMenu;
    public GameObject maps;
    public TMP_Text errorText;

    async void Start()
    {
        playButton.interactable = false;
        LoadedClipName = null;
        LoadedClip = null;
        errorText.gameObject.SetActive(false);
        rhythmEngine.StartGame();

        NativeFilePicker.Permission permission = await NativeFilePicker.RequestPermissionAsync(true);
        Debug.Log($"Permission result: {permission}");
    }

    public void LoadSong()
    {
        OpenFilePicker();
    }

    void OpenFilePicker()
    {
        NativeFilePicker.PickFile(path =>
        {
            if (path == null)
            {
                Debug.Log("Operation cancelled");
                return;
            }
            Debug.Log($"Picked file: {path}");
            LoadedClipName = System.IO.Path.GetFileNameWithoutExtension(path);
            StartCoroutine(LoadAudio(path));
        }, new string[] { "audio/*" });
    }

    IEnumerator LoadAudio(string path)
    {
        errorText.gameObject.SetActive(false);

        AudioType audioType = GetAudioType(path);
        string url = "file:///" + path.Replace("\\", "/");

        using var req = UnityWebRequestMultimedia.GetAudioClip(url, audioType);
        ((DownloadHandlerAudioClip)req.downloadHandler).compressed = false;
        yield return req.SendWebRequest();

        if (req.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"Ошибка загрузки: {req.error}");
            errorText.text = "Failed to load file.\nPlease try another one.";
            errorText.gameObject.SetActive(true);
            yield break;
        }

        AudioClip clip = DownloadHandlerAudioClip.GetContent(req);

        if (clip == null || clip.samples == 0 || clip.loadState == AudioDataLoadState.Failed)
        {
            Debug.LogError("Клип не загрузился");
            errorText.text = "Failed to load file.\nPlease try another one.";
            errorText.gameObject.SetActive(true);
            yield break;
        }

        playButton.interactable = true;
        LoadedClip = clip;
        Debug.Log($"Загружено: {LoadedClipName} | samples: {clip.samples}");
    }

    AudioType GetAudioType(string path)
    {
        string ext = System.IO.Path.GetExtension(path).ToLower();
        return ext switch
        {
            ".mp3" => AudioType.MPEG,
            ".wav" => AudioType.WAV,
            ".ogg" => AudioType.OGGVORBIS,
            _ => AudioType.UNKNOWN
        };
    }

    public void OpenLoadMenu()
    {
        maps.SetActive(false);
        loadMenu.SetActive(true);
        errorText.gameObject.SetActive(false);
    }

    public void CloseLoadMenu()
    {
        maps.SetActive(true);
        loadMenu.SetActive(false);
    }

    public void PlayCustomMap()
    {
        if (LoadedClip == null) return;
        PauseManager.needToAnalyseMap = true;
        SceneManager.LoadScene(2);
    }
}