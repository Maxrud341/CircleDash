using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorTransition : MonoBehaviour
{
    public Color redColor = Color.red;
    public Color yellowColor = Color.yellow;
    public Color greenColor = Color.green;

    public Color currentColor;

    private SpriteRenderer spriteRenderer;


    private int bitCounter = 0;


    public TransitionState currentState = TransitionState.Default;

    void OnEnable()
    {
        RhythmEngine.OnBitEvent += OnBitTriggered;
    }

    void OnDisable()
    {
        RhythmEngine.OnBitEvent -= OnBitTriggered;
    }

    void OnBitTriggered()
    {
        bitCounter++;
    }
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        switch (currentState)
        {
            case TransitionState.Red:
                currentColor = redColor;
                spriteRenderer.color = currentColor;
                break;

            case TransitionState.Transit:
                if (RhythmEngine.boolOnBit)
                {
                    currentColor = greenColor;
                    spriteRenderer.color = currentColor;
                }
                else
                {
                    float t = Math.Abs(RhythmEngine.accuracy);
                    currentColor = Color.Lerp(redColor, yellowColor, t);
                    spriteRenderer.color = currentColor;
                }
                break;

            case TransitionState.Default:
                currentColor = Color.white;
                spriteRenderer.color = currentColor;
                break;
            default:
                Debug.Log("ERROR COLOR STATE");
                return;
        }

    }

}

public enum TransitionState
{
    Default,
    Red,
    Transit
}
