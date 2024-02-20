using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class MapSettings : MonoBehaviour
{
    public int sceneNum;
    public Color color;
    public Sprite bg;
    public SpriteRenderer bgObj;
    public AudioClip track;
    public AudioSource audioSource;
    public int bpm;
    
    
    public GameObject arrowGO;
    public GameObject arrows;
    
    public ArrowsGenerator arrowsGenerator;
    public UnityEngine.UI.Image[] colorImg;
    public Pause pause;

    private Arrow[] ArrowMap;
        private void Awake() {
            loadScene();    
        }
        public void loadScene(){
            int songLength = (int)track.length;
            float bitDelay = 60f / bpm;
            int numberOfBeats = (int)(songLength / bitDelay)-20;
            audioSource.clip = track;
            audioSource.Play();

            

            ArrowMap = MapGenerator.GenerateArrowMap(bitDelay, numberOfBeats, arrowGO);
            arrowsGenerator.GenerateMap(ArrowMap);

            StartCoroutine(MapGenerator.RepeatCoroutineFunction(numberOfBeats,  bitDelay,  arrows));


            foreach (UnityEngine.UI.Image img in colorImg)
            {
                img.color = color;   
            }

            bgObj.sprite = bg;

            pause.sceneNum = sceneNum;
        }



    }
