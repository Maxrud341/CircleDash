using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowGenerator : MonoBehaviour
{
    public bool FullSpeed = false;

    public GameObject Arrow;
    public GameObject Arrows;
    bool songEnded = false;
    private List<int> currentPattern;
    private int currentPatternIndex = 0;
    int num = 0;
    void OnEnable()
    {
        RhythmEngine.OnBitEvent += OnBitTriggered;
        RhythmEngine.OnSongEndEvent += OnSongEnd;
    }

    void OnDisable()
    {
        RhythmEngine.OnBitEvent -= OnBitTriggered;
        RhythmEngine.OnSongEndEvent -= OnSongEnd;
    }

    void OnSongEnd()
    {
        Debug.Log("Song has ended!");
        songEnded = true;
    }

    void OnBitTriggered()
    {
        if (num > 2 && (num % 2 == 0 || FullSpeed))
        {
            if (!songEnded)
            {
                if (currentPattern == null || currentPatternIndex >= currentPattern.Count)
                {
                    int firstDirection = Random.Range(1, 5);
                    int secondDirection;
                    do
                    {
                        secondDirection = Random.Range(1, 5);
                    } while (secondDirection == firstDirection);

                    currentPattern = new List<int>();
                    //int repetitions = Random.Range(4, 8);
                    int repetitions = 4;
                    for (int i = 0; i < repetitions; i++)
                    {
                        currentPattern.Add(firstDirection);
                        currentPattern.Add(secondDirection);
                    }

                    currentPatternIndex = 0;
                }

                int randDirection = currentPattern[currentPatternIndex];
                currentPatternIndex++;
                Quaternion rotation = Quaternion.Euler(0, 0, -90 * (randDirection - 1));
                
                GameObject newArrow = Instantiate(Arrow, transform.position, rotation);
                ArrowProperties arrowProperties = newArrow.GetComponent<ArrowProperties>();
                arrowProperties.direction = randDirection;
                newArrow.transform.SetParent(Arrows.transform);
            }
        } else {
            
        }
        num++;
    }
}
