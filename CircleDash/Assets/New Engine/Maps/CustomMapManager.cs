using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Networking;
#if UNITY_ANDROID
using UnityEngine.Android;
#endif

public class CustomMapManager : MonoBehaviour
{
    public RhythmEngine rhythmEngine;
    public static AudioClip LoadedClip;
    public static string LoadedClipName;
    public Button playButton;
    public GameObject loadMenu;
    public GameObject maps;

    public void LoadSong()
    {
#if UNITY_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageRead);
            return;
        }
#endif
        NativeFilePicker.PickFile(path =>
        {
            if (path == null) return;
            LoadedClipName = System.IO.Path.GetFileNameWithoutExtension(path);
            StartCoroutine(LoadAudio(path));
        }, new string[] { "audio/*" });
    }

    IEnumerator LoadAudio(string path)
    {
        AudioType audioType = GetAudioType(path);
        string url = "file:///" + path.Replace("\\", "/");

        using var req = UnityWebRequestMultimedia.GetAudioClip(url, audioType);
        ((DownloadHandlerAudioClip)req.downloadHandler).compressed = false;
        yield return req.SendWebRequest();

        if (req.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"Ошибка загрузки: {req.error}");
            yield break;
        }

        AudioClip clip = DownloadHandlerAudioClip.GetContent(req);

        if (clip == null || clip.loadState == AudioDataLoadState.Failed)
        {
            Debug.LogError("Клип не загрузился");
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
    }

    public void CloseLoadMenu()
    {

        maps.SetActive(true);
        loadMenu.SetActive(false);
    }

    public void PlayCustomMap()
    {
        if (LoadedClip == null) return;
        SceneManager.LoadScene(2);
    }

    void Start()
    {
        playButton.interactable = false;
        LoadedClipName = null;
        LoadedClip = null;
        rhythmEngine.StartGame();
    }
}