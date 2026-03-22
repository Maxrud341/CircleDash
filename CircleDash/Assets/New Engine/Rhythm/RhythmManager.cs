using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmManager : MonoBehaviour
{
    public ArrowGenerator arrowGenerator;
    public RhythmEngine rhythmEngine;
    public AudioSource audioSource;

    public List<Vector2> X2_Sections; // Секции с включением FullSpeed

    void Start()
    {
        StartCoroutine(ManageFullSpeed());
    }

    public IEnumerator ManageFullSpeed()
    {
        foreach (var section in X2_Sections)
        {
            float sectionStartTime = section.x;
            float sectionEndTime = section.y;

            // Ждём до начала секции
            float currentTime = audioSource.time;
            float waitUntilStart = Mathf.Max(sectionStartTime - currentTime - 6 * RhythmEngine.bitDelay, 0);
            yield return new WaitForSeconds(waitUntilStart);

            // Включаем FullSpeed
            arrowGenerator.FullSpeed = true;

            // Ждём до конца секции
            float sectionDuration = Mathf.Max(sectionEndTime - sectionStartTime - 6 * RhythmEngine.bitDelay, 0);
            yield return new WaitForSeconds(sectionDuration);

            // Отключаем FullSpeed
            arrowGenerator.FullSpeed = false;
        }
    }
}
