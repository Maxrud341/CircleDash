using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectHit : MonoBehaviour
{
    public ManageRangeArrow2Trap manageRangeArrow2Trap;
    public Destroyer destroyer;
    public Score score;
    public int hitScore;
    public AudioSource audioSource;

    public void tryDirection(int direction)
    {
        hitScore = (int)((1 - manageRangeArrow2Trap.normalizedDistance) * 100);
        if (hitScore > 70 && destroyer.direction == direction)
        {
            Debug.Log("SUCCESS!!");
            Debug.Log(hitScore);
            score.EditScore(hitScore);
            audioSource.Play();
            Destroy(destroyer.currentArrow);
            
        }
    }

}
