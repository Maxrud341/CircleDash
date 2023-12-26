using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowReaction : MonoBehaviour
{
    public PlatesReaction platesReaction;
    private Color color;
    private SpriteRenderer arrowTop;
    private SpriteRenderer arrowBot;

    void Update()
    {
        if (arrowTop != null && arrowBot != null)
        {
            color = platesReaction.plateColor;
            arrowTop.color = color;
            arrowBot.color = color;
        }
    }

    public void ArrowsReaction(GameObject currentArrow){
        arrowTop = currentArrow.transform.GetChild(0).GetComponent<SpriteRenderer>();
        arrowBot = currentArrow.transform.GetChild(1).GetComponent<SpriteRenderer>();
    }
}
