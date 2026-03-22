using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;


public class PauseManager : MonoBehaviour
{
    public bool isCustomMap;
    public RhythmEngine rhythmEngine;
    public RhythmManager rhythmManager;
    public GameObject menuCanvas;
    public GameObject LoseCanv;
    public GameObject WinCanv;
    public GameObject SettingsCanv;
    public GameObject EndCanv;
    public GameObject MenuIco;
    public AudioSource music;
    public RhythmJoystick joystick;
    public GameObject unPauseTimer;
    public int sceneNum;
    public static bool needToAnalyseMap = true;
    private static List<Vector2> sections;
    private static int bpm;
    public void OpenMenu()
    {
        menuCanvas.SetActive(true);
    }

    public void CloseMenu()
    {
        menuCanvas.SetActive(false);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        joystick.enabled = false;
    }
    public void MusicPause()
    {
        music.Pause();
    }
    public void MusicUnPause()
    {
        music.UnPause();
    }
    public void UnPauseGame()
    {
        Time.timeScale = 1f;
        joystick.enabled = true;
    }

    public void StartUnPauseTimer()
    {
        unPauseTimer.SetActive(true);
    }
    public void backToMaps()
    {
        FadeIn();
        StartCoroutine(LoadSceneAfterFade());
    }

    private IEnumerator LoadSceneAfterFade()
    {
        yield return new WaitForSecondsRealtime(0.6f);
        UnPauseGame();
        SceneManager.LoadScene(0);
    }

    public void RestartGame()
    {
        needToAnalyseMap = false;
        SceneManager.LoadScene(sceneNum);
        UnPauseGame();
    }

    public void Test()
    {
        Debug.Log("Test");
    }

    public void OpenLoseCanv()
    {
        LoseCanv.SetActive(true);
    }

    public void OpenWinCanv()
    {
        WinCanv.SetActive(true);
    }

    public void OpenSettingsCanv()
    {
        SettingsCanv.SetActive(true);
    }

    public void CloseSettingsCanv()
    {
        SettingsCanv.SetActive(false);
    }

    public void CloseMenuIco()
    {
        MenuIco.SetActive(false);
    }

    public void OpenMenuIco()
    {
        MenuIco.SetActive(true);
    }

    public void OpenEndMenu()
    {
        EndCanv.SetActive(true);
    }

    public Animator fadeBlack;
    void Start()
    {
        if (isCustomMap)
        {
            if (needToAnalyseMap)
            {
                (sections, bpm) = MusicAnalyser.AnalyseClip(CustomMapManager.LoadedClip);

            }

            rhythmEngine.bpm = bpm;
            rhythmEngine.songClip = CustomMapManager.LoadedClip;
            rhythmManager.X2_Sections = sections;
            needToAnalyseMap = false;
            Debug.Log($"Map analysed: BPM={bpm}, Sections count={sections.Count}");

            rhythmEngine.StartGame();
            rhythmManager.StartRhythmManager();
        }
        else
        {
            rhythmEngine.StartGame();
            rhythmManager.StartRhythmManager();
        }
        fadeBlack.updateMode = AnimatorUpdateMode.UnscaledTime;
        fadeBlack.SetTrigger("StartOut");
    }

    public void FadeIn()
    {
        fadeBlack.SetTrigger("IN");
    }

}
