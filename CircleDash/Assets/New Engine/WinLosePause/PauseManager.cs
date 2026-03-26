using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.Threading;


public class PauseManager : MonoBehaviour
{
    public bool isCustomMap;
    public GameObject loadingScreen;
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
    void Start() => StartCoroutine(InitRoutine());

    IEnumerator InitRoutine()
    {
        Time.timeScale = 0f;

        if (isCustomMap)
        {
            if (needToAnalyseMap)
            {
                loadingScreen.SetActive(true);

                // Всё что касается AudioClip — на главном потоке
                float[] rawSamples = new float[CustomMapManager.LoadedClip.samples * CustomMapManager.LoadedClip.channels];
                CustomMapManager.LoadedClip.GetData(rawSamples, 0);
                int ch = CustomMapManager.LoadedClip.channels;
                int freq = CustomMapManager.LoadedClip.frequency;
                int sampleCount = CustomMapManager.LoadedClip.samples;
                int bpmResult = UniBpmAnalyzer.AnalyzeBpm(CustomMapManager.LoadedClip);

                bool done = false;
                new Thread(() =>
                {
                    sections = MusicAnalyser.Analyse(rawSamples, ch, sampleCount, freq,
                                2048, 512, 120, 64, 20f, 0.6f, 300, 1.6f, 15, 50, 5f, 2f);
                    bpm = bpmResult;

                    done = true;
                }).Start();

                while (!done) yield return null;

                loadingScreen.SetActive(false);
                needToAnalyseMap = false;
            }
            else
            {
                Debug.Log("Analysis skipped (needToAnalyseMap=false)");
            }

            Debug.Log("Applying map data to rhythm engine...");
            if (bpm < 100) bpm *= 2;
            if (bpm > 180) bpm /= 2;
            rhythmEngine.bpm = bpm;
            rhythmEngine.songClip = CustomMapManager.LoadedClip;
            rhythmManager.X2_Sections = sections;
            Debug.Log($"Map analysed: BPM={bpm}, Sections count={sections.Count}");
        }

        Time.timeScale = 1f;

        if (isCustomMap)
        {
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
