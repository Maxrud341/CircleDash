using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    public Scrollbar scrollbarMusic;
    public Scrollbar scrollbarFx;
    public AudioSource music;
    public AudioSource[] FX;
    void Update()
    {
        music.volume = scrollbarMusic.value;
        foreach (AudioSource fx in FX)
        {
            fx.volume = scrollbarFx.value;
        }    
    }
}
