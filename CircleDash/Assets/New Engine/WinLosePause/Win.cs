using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    
    void OnEnable()
    {
        RhythmEngine.OnSongEndEvent += OnWin;
    }

    void OnDisable()
    {
        RhythmEngine.OnSongEndEvent -= OnWin;
    }

    void OnWin(){

    }
}
