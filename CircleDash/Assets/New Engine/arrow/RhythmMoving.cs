using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmMoving : MonoBehaviour
{
    public float moveDistance;
    public float animationmX = 4;
    // private float animationTime = RhythmEngine.bitDelay / animationmX;

    // private Animator animator;

    public bool isActive = true;
    void OnEnable()
    {
        RhythmEngine.OnBitEvent += OnBitTriggered;
        RhythmEngine.OnSongEndEvent += OnSongEnd;
    }

    void OnDisable()
    {
        RhythmEngine.OnBitEvent -= OnBitTriggered;
        RhythmEngine.OnSongEndEvent -= OnSongEnd;
    }

    void OnSongEnd()
    {
    }


    // void Start()
    // {
    //     // Получаем компонент Animator
    //     animator = GetComponent<Animator>();
    // }

    void OnBitTriggered()
    {
        // if (isActive && animator != null)
        // {
        //     float animationSpeed;

        //     if (RhythmEngine.bitDelay > 0)
        //     {
        //         animationSpeed = animationmX / RhythmEngine.bitDelay;
        //     }
        //     else
        //     {
        //         animationSpeed = 1;
        //     }

        //     animator.speed = animationSpeed;

        //     animator.SetTrigger("MoveArrow");
        // }

        if (isActive)
        {
            // animationTime = Mathf.Clamp(animationTime, 0.0f, RhythmEngine.bitDelay);
            StartCoroutine(MoveOverTime(moveDistance, RhythmEngine.bitDelay / animationmX));
        }
    }



    private IEnumerator MoveOverTime(float distance, float timeToMove)
    {
        float delay = RhythmEngine.bitDelay / 4;

        if (delay > 0)
        {
            yield return new WaitForSeconds(delay);
        }

        float elapsedTime = 0;
        Vector3 startingPosition = transform.position;
        Vector3 targetPosition = startingPosition + new Vector3(distance, 0, 0);

        while (elapsedTime < timeToMove)
        {
            transform.position = Vector3.Lerp(startingPosition, targetPosition, elapsedTime / timeToMove);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
    }
}
