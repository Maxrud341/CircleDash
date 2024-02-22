using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectHit : MonoBehaviour
{
    public OnHit onHit;
    public ManageRangeArrow2Trap manageRangeArrow2Trap;
    public Destroyer destroyer;
    public int hitScore;
    public End end;



    public void tryDirection(int direction)
    {
        hitScore = (int)((1 - manageRangeArrow2Trap.normalizedDistance) * 10);
        if (hitScore > 7 && destroyer.direction == direction)
        {
            onHit.OnSuccessHit(direction, hitScore);
            Destroy(destroyer.currentArrow);
            if (hitScore > 8.5f)
            {
                end.greenArrow++;
            }
            else
            {
                end.orangeArrow++;
            }
        }
        else
        {
            onHit.OnUnsuccessHit(direction, hitScore);
        }
    }



}
