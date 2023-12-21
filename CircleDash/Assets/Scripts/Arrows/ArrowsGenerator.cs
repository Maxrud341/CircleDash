using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowsGenerator : MonoBehaviour
{
    public PlatesReaction platesReaction;
    public Arrow[] arrows;
    void Start()
    {
        StartCoroutine(SpawnArrowsWithDelay());
    }

    private IEnumerator SpawnArrowsWithDelay()
    {
        foreach (Arrow arrow in arrows)
        {
            yield return new WaitForSeconds(arrow.delay);
            createArrow(arrow);
            
        }
    }

    private void createArrow(Arrow arrow)
    {
        Quaternion rotation = Quaternion.Euler(0, 0, 0);

        switch (arrow.derection)
        {
            case 1:
                rotation = Quaternion.Euler(0, 0, 0);
                Debug.Log("up");
                break;
            case 2:
                rotation = Quaternion.Euler(0, 0, -90);
                Debug.Log("right");
                break;
            case 3:
                rotation = Quaternion.Euler(0, 0, -180);
                Debug.Log("down");
                break;
            case 4:
                rotation = Quaternion.Euler(0, 0, -270);
                Debug.Log("left");
                break;
            default:
                rotation = Quaternion.Euler(0, 0, 0);
                Debug.Log("ERROR DIRECTION ARROW");
                break;
        }

        GameObject newArrow = Instantiate(arrow.arrow, transform.position, rotation);

        newArrow.transform.SetParent(gameObject.transform);
        

    }
}
