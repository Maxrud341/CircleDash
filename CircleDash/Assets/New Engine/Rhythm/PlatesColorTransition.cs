using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlatesColorTransition : MonoBehaviour
{
    public Sprite defaultSprite;
    public Sprite glowSprite;

    public GameObject topPlate;
    public GameObject rightPlate;
    public GameObject botPlate;
    public GameObject leftPlate;


    private ColorTransition topPlateCT;
    private ColorTransition rightPlateCT;
    private ColorTransition botPlateCT;
    private ColorTransition leftPlateCT;

    private SpriteRenderer topPlateSR;
    private SpriteRenderer rightPlateSR;
    private SpriteRenderer botPlateSR;
    private SpriteRenderer leftPlateSR;


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

        topPlateSR = topPlate.GetComponent<SpriteRenderer>();
        rightPlateSR = rightPlate.GetComponent<SpriteRenderer>();
        botPlateSR = botPlate.GetComponent<SpriteRenderer>();
        leftPlateSR = leftPlate.GetComponent<SpriteRenderer>();

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
                    topPlateSR.sprite = glowSprite;
                    topPlateCT.currentState = TransitionState.Transit;
                    break;
                case 2:
                    rightPlateSR.sprite = glowSprite;
                    rightPlateCT.currentState = TransitionState.Transit;
                    break;
                case 3:
                    botPlateSR.sprite = glowSprite;
                    botPlateCT.currentState = TransitionState.Transit;
                    break;
                case 4:
                    leftPlateSR.sprite = glowSprite;
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
                    topPlateSR.sprite = glowSprite;
                    topPlateCT.currentState = TransitionState.Red;

                    break;
                case 2:
                    rightPlateSR.sprite = glowSprite;
                    rightPlateCT.currentState = TransitionState.Red;
                    break;
                case 3:
                    botPlateSR.sprite = glowSprite;
                    botPlateCT.currentState = TransitionState.Red;
                    break;
                case 4:
                    leftPlateSR.sprite = glowSprite;
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
        topPlateSR.sprite = defaultSprite;
        rightPlateSR.sprite = defaultSprite;
        botPlateSR.sprite = defaultSprite;
        leftPlateSR.sprite = defaultSprite;

        topPlateCT.currentState = TransitionState.Default;
        rightPlateCT.currentState = TransitionState.Default;
        botPlateCT.currentState = TransitionState.Default;
        leftPlateCT.currentState = TransitionState.Default;
    }

}
