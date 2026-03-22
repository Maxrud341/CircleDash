using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class PauseManager : MonoBehaviour
{
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
        fadeBlack.updateMode = AnimatorUpdateMode.UnscaledTime;
        fadeBlack.SetTrigger("StartOut");
    }

    public void FadeIn()
    {
        fadeBlack.SetTrigger("IN");
    }

}
