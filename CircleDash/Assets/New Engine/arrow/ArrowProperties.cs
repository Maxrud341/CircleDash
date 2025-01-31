using System;
using UnityEngine;

public class ArrowProperties : MonoBehaviour
{
    public bool isCurrent;
    public int direction;
    public int bitCounter = 0;
    public bool canBeCurrent = false;
    public bool canBeHitted = false;
    public static event Action OnArrowDestroy;
    public static event Action OnArrowSkip;
    private GameObject arrowTexture;


    public ColorTransition rhythmColorTransition;

    void OnEnable()
    {
        RhythmEngine.OnBitEvent += OnBitTriggered;
        RhythmEngine.OnBetweenBit += OnBetweenBit;

    }

    void OnDisable()
    {
        RhythmEngine.OnBitEvent -= OnBitTriggered;
        RhythmEngine.OnBetweenBit -= OnBetweenBit;

    }

    void OnBitTriggered()
    {
        bitCounter++;
        canBeCurrent = true;
    }

    void OnBetweenBit()
    {
        if (canBeHitted == false && bitCounter == 5)
        {
            canBeHitted = true;
        }

        if (bitCounter == 6 && RhythmEngine.accuracy <= 0.2)
        {
            OnArrowSkip?.Invoke();
            DestroySelf();
        }
    }
    void Start()
    {
        arrowTexture =  transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
        rhythmColorTransition = arrowTexture.GetComponent<ColorTransition>();
    }
    void Update()
    {


        if (isCurrent)
        {

            if (canBeHitted)
            {
                arrowTexture.SetActive(true);
                rhythmColorTransition.currentState = TransitionState.Transit;
            }
            else
            {
                arrowTexture.SetActive(true);
                rhythmColorTransition.currentState = TransitionState.Red;
            }

        }


    }

    public void DestroySelf()
    {
        isCurrent = false;
        canBeCurrent = false;
        OnArrowDestroy?.Invoke();
        Destroy(gameObject);
    }
}
