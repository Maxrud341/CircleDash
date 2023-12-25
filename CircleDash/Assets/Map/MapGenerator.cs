using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public AudioClip  track;
    public int bpm;
    public int trackLength;
    public float bitDelay;
    public int numberOfBeats;

    public ArrowsGenerator arrowsGenerator;

    public GameObject arrowGO;
    public Arrow[] ArrowMap;

    private void Start() {
        int songLength = (int)track.length;
        Debug.Log(songLength);

        bitDelay = 60f / bpm;
        Debug.Log(bitDelay);

        numberOfBeats = (int)(songLength / bitDelay)-20;
        Debug.Log(numberOfBeats);

        ArrowMap = GenerateArrowMap(bitDelay, numberOfBeats, arrowGO);
        arrowsGenerator.GenerateMap(ArrowMap);
    }

    public static Arrow[] GenerateArrowMap(float bitDelay, int numberOfBeats, GameObject arrowGO){
        Arrow[] ArrowMap = new Arrow[numberOfBeats];

        for (int i = 0; i < numberOfBeats; i++)
        {
            Arrow arrow = new Arrow();
            arrow.arrow = arrowGO;
            arrow.delay = bitDelay; 
            arrow.direction = Random.Range(1, 5);

            ArrowMap[i] = arrow;
        }
        ArrowMap[0].delay = 0;
        return ArrowMap;

    }
}
