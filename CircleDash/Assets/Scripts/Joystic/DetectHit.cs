using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectHit : MonoBehaviour
{
    public OnHit onHit;
    public ManageRangeArrow2Trap manageRangeArrow2Trap;
    public Destroyer destroyer;
    public int hitScore;



    public void tryDirection(int direction)
    {
        hitScore = (int)((1 - manageRangeArrow2Trap.normalizedDistance) * 100);
        if (hitScore > 70 && destroyer.direction == direction)
        {
            onHit.OnSuccessHit(direction, hitScore);
            Destroy(destroyer.currentArrow);
        }
        else
        {
            onHit.OnUnsuccessHit(direction, hitScore);   
        }
    }

    

}
