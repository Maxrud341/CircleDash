using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{

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
        ArrowMap[0].delay =  bitDelay - ((3.36f/2f)*2.5f % bitDelay)+(bitDelay*3);
        return ArrowMap;

    }

    public static IEnumerator RepeatCoroutineFunction(int numberOfBeats, float bitDelay, GameObject arrows)
    {
        for (int i = 0; i < numberOfBeats; i++)
        {
            yield return new WaitForSeconds(bitDelay);
             foreach (Transform child in arrows.transform)
            {
                child.GetComponent<Animator>().SetTrigger("biggerArrow");
            }
        }
    }
}
