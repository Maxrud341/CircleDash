using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject menuCanvas;
    public GameObject LoseCanv;
    public GameObject SettingsCanv;
    public GameObject EndCanv;
    public GameObject MenuIco;
    public AudioSource music;
    public Joystick3 joystick3;
    public int sceneNum;
    public void OpenMenu()
    {
        menuCanvas.SetActive(true);
    }

    public void CloseMenu()
    {
        menuCanvas.SetActive(false);
    }

    public void PauseTime()
    {
        Time.timeScale = 0f;
        joystick3.enabled = false;
    }
    public void PauseMusic()
    {
        music.Pause();
    }
    public void UnPauseMusic()
    {
        music.UnPause();
    }
    public void UnPauseTime()
    {
        Time.timeScale = 1f;
        joystick3.enabled = true;
    }
    public void backToMaps()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(sceneNum);
    }

    public void Test()
    {
        Debug.Log("Test");
    }

    public void OpenLoseCanv()
    {
        LoseCanv.SetActive(true);
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

    public void PauseGame()
    {
        PauseTime();
        PauseMusic();
        CloseMenuIco();
        OpenMenu();
    }

    public void UnPauseGame()
    {
        UnPauseTime();
        UnPauseMusic();
        OpenMenuIco();
        CloseMenu();
    }



    void OnApplicationPause(bool pauseStatus)
    {
        PauseGame();
    }
}
