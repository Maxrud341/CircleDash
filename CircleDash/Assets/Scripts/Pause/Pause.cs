using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject menuCanvas;
    public AudioSource music;
    public Joystick3 joystick3;
    public int mapIndex = 1;
    public void OpenMenu(){
        menuCanvas.SetActive(true);
    }

    public void CloseMenu(){
        menuCanvas.SetActive(false);
    }

    public void PauseGame(){
        Time.timeScale = 0f;
        music.Pause();
        joystick3.enabled = false;
    }

    public void UnPauseGame(){
        Time.timeScale = 1f;
        music.UnPause();
        joystick3.enabled = true;
    }
    public void backToMaps(){
        SceneManager.LoadScene(0);    
    }

    public void RestartGame(){
        SceneManager.LoadScene(mapIndex);
    }

    public void Test(){
        Debug.Log("Test");
    }
}
