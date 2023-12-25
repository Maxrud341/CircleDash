using System.Collections;
using UnityEngine;

public class ArrowsGenerator : MonoBehaviour
{
    public PlatesReaction platesReaction;
    public Arrow[] arrows;

    public void GenerateMap(Arrow[] arrowsMap)
    {
        arrows = arrowsMap;
        StartCoroutine(SpawnArrowsWithDelay());
    }

    private IEnumerator SpawnArrowsWithDelay()
    {
        foreach (Arrow arrow in arrows)
        {
            yield return new WaitForSeconds(arrow.delay);
            CreateArrow(arrow);
        }
    }

    private void CreateArrow(Arrow arrow)
    {
        Quaternion rotation = Quaternion.Euler(0, 0, -90 * (arrow.direction - 1));

        GameObject newArrow = Instantiate(arrow.arrow, transform.position, rotation, transform);
    }
}
