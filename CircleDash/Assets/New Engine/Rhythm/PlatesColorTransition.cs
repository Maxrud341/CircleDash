using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlatesColorTransition : MonoBehaviour
{
    public GameObject topPlate;
    public GameObject rightPlate;
    public GameObject botPlate;
    public GameObject leftPlate;




    private ColorTransition topPlateCT;
    private ColorTransition rightPlateCT;
    private ColorTransition botPlateCT;
    private ColorTransition leftPlateCT;



    private ArrowProperties currentArrow;

    private void OnEnable()
    {
        CurrentArrowManager.OnNewCurrentArrow += OnNewCurrentArrow;
        ArrowProperties.OnArrowDestroy += OnArrowDestroy;
    }

    private void OnDisable()
    {
        CurrentArrowManager.OnNewCurrentArrow -= OnNewCurrentArrow;
        ArrowProperties.OnArrowDestroy -= OnArrowDestroy;

    }

    private void Start()
    {
        topPlateCT = topPlate.GetComponent<ColorTransition>();
        rightPlateCT = rightPlate.GetComponent<ColorTransition>();
        botPlateCT = botPlate.GetComponent<ColorTransition>();
        leftPlateCT = leftPlate.GetComponent<ColorTransition>();
    }
    void OnNewCurrentArrow(GameObject arrow)
    {
        currentArrow = arrow.GetComponent<ArrowProperties>();
        switchPlate(currentArrow.direction);
    }

    private void Update()
    {
        if (currentArrow != null && currentArrow.canBeHitted)
        {
            switchPlate(currentArrow.direction);
        }
    }

    void OnArrowDestroy()
    {
        resetPlates();
        currentArrow = null;
    }
    public void switchPlate(int direction)
    {
        resetPlates();
        if (currentArrow != null && currentArrow.canBeHitted)
        {

            switch (direction)
            {
                case 1:
                    topPlateCT.currentState = TransitionState.Transit;
                    break;
                case 2:
                    rightPlateCT.currentState = TransitionState.Transit;
                    break;
                case 3:
                    botPlateCT.currentState = TransitionState.Transit;
                    break;
                case 4:
                    leftPlateCT.currentState = TransitionState.Transit;
                    break;
                default:
                    Debug.Log("ERROR DIRECTION PLATE");
                    return;
            }
        }
        else
        {
            switch (direction)
            {
                case 1:
                    topPlateCT.currentState = TransitionState.Red;
                    break;
                case 2:
                    rightPlateCT.currentState = TransitionState.Red;
                    break;
                case 3:
                    botPlateCT.currentState = TransitionState.Red;
                    break;
                case 4:
                    leftPlateCT.currentState = TransitionState.Red;
                    break;
                default:
                    Debug.Log("ERROR DIRECTION PLATE");
                    return;
            }
        }
    }

    public void resetPlates()
    {
        topPlateCT.currentState = TransitionState.Default;
        rightPlateCT.currentState = TransitionState.Default;
        botPlateCT.currentState = TransitionState.Default;
        leftPlateCT.currentState = TransitionState.Default;

    }
}
