using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesReaction : MonoBehaviour
{
    public ManageRangeArrow2Trap manageRangeArrow2Trap;

    public GameObject topPlate;
    public GameObject rightPlate;
    public GameObject botPlate;
    public GameObject leftPlate;


    private SpriteRenderer topPlateSR;
    private SpriteRenderer rightPlateSR;
    private SpriteRenderer botPlateSR;
    private SpriteRenderer leftPlateSR;

    private SpriteRenderer currentPlateSR;
    public Color plateColor;


    private bool plateSwiched = false;
    public float transitionValue;
    private void Start()
    {
        topPlateSR = topPlate.GetComponent<SpriteRenderer>();
        rightPlateSR = rightPlate.GetComponent<SpriteRenderer>();
        botPlateSR = botPlate.GetComponent<SpriteRenderer>();
        leftPlateSR = leftPlate.GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (plateSwiched)
        {
            transitionValue = manageRangeArrow2Trap.normalizedDistance;
            if(transitionValue < 0.15){
                plateColor = Color.green;
                currentPlateSR.color = plateColor;
            }else{
                plateColor = Color.Lerp(Color.yellow, Color.red, transitionValue);
                currentPlateSR.color = plateColor;

            }
        }
    }
    public void switchPlate(int direction)
    {
        plateSwiched = true;
        resetPlates();

        switch (direction)
        {
            case 1:
                currentPlateSR = topPlateSR;
                break;
            case 2:
                currentPlateSR = rightPlateSR;
                break;
            case 3:
                currentPlateSR = botPlateSR;
                break;
            case 4:
                currentPlateSR = leftPlateSR;
                break;
            default:
                Debug.Log("ERROR DIRECTION PLATE");
                return;
        }
    }

    public void resetPlates(){
        transitionValue = 0;

        topPlateSR.color = Color.white;
        rightPlateSR.color = Color.white;
        botPlateSR.color = Color.white;
        leftPlateSR.color = Color.white;
    }
}
