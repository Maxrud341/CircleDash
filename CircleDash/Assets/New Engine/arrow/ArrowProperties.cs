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
    
    public ColorTransition rhythmColorTransition;

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
        canBeCurrent = true;
    }
    void Start()
    {
        rhythmColorTransition = transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<ColorTransition>();
    }
    void Update()
    {
        if (canBeHitted == false && bitCounter == 5 && RhythmEngine.accuracy <= 0.05)
        {
            canBeHitted = true;
        }

        if (isCurrent)
        {

            if (canBeHitted)
            {
                rhythmColorTransition.currentState = TransitionState.Transit;
            }
            else
            {
                rhythmColorTransition.currentState = TransitionState.Red;
            }

        }




        if (bitCounter == 6 && RhythmEngine.accuracy <= 0.05)
        {
            OnArrowSkip?.Invoke();
            DestroySelf();
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
