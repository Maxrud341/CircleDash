using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lose : MonoBehaviour
{
    public PauseManager pause;

    void OnEnable()
    {
        Lives.OnLose += OnLose;
    }

    void OnDisable()
    {
        Lives.OnLose -= OnLose;
    }

    void OnLose()
    {
        pause.PauseGame();
        pause.MusicPause();
        pause.OpenLoseCanv();
        pause.CloseMenuIco();
    }
}
