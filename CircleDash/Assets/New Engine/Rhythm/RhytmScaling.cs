using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhytmScaling : MonoBehaviour
{
    public bool canScale = true;
    public Animator[] animators;
    void OnEnable()
    {
        RhythmEngine.OnBitEvent += OnBitTriggered;
    }

    void OnDisable()
    {
        RhythmEngine.OnBitEvent -= OnBitTriggered;
    }

    void OnSongEnd()
    {

    }

    void OnBitTriggered()
    {
        if (canScale && animators != null)
        {

            for (int i = 0; i < animators.Length; i++)
            {
                StartCoroutine(Scale(RhythmEngine.bitDelay / 5f, animators[i]));
            }
        }



    }
    private IEnumerator Scale(float timeToScale, Animator animator)
    {
        float delay = RhythmEngine.bitDelay - timeToScale;
        if (delay > 0)
        {
            yield return new WaitForSeconds(delay);
        }

        animator.speed = 1 / timeToScale;

        animator.SetTrigger("Scale");

    }
}