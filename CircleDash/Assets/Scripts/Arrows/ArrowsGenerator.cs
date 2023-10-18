using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowsGenerator : MonoBehaviour
{
    public Arrow[] arrows;
    void Start()
    {
        StartCoroutine(SpawnArrowsWithDelay());
    }

    private IEnumerator SpawnArrowsWithDelay()
    {
        foreach (Arrow arrow in arrows)
        {
            // Ждем заданное время
            yield return new WaitForSeconds(arrow.delay);

            // Создаем стрелу
            Instantiate(arrow.arrow, transform.position, Quaternion.identity);
        }
    }
}
