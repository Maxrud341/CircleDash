using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowRhythmScaling : MonoBehaviour
{
    //public float scaleIncreaseFactor;
    private float animationTime = RhythmEngine.bitDelay / 5;
    public bool canScale = true;
    private Animator animator;
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

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnBitTriggered()
    {
        if (canScale && animator != null)
        {
            // float animationSpeed;

            // if (RhythmEngine.bitDelay > 0)
            // {
            //     animationSpeed = animationmX / RhythmEngine.bitDelay;
            // }
            // else
            // {
            //     animationSpeed = 1;
            // }

            // animator.speed = 1 / animationTime;

            // animator.SetTrigger("Scale");
            StartCoroutine(Scale(animationTime));
        }

        // if (isActive)
        // {
        //     animationTime = Mathf.Clamp(animationTime, 0.0f, RhythmEngine.bitDelay);
        //     StartCoroutine(ScaleOverTime(scaleIncreaseFactor, animationTime));
        // }

    }
    private IEnumerator Scale(float timeToScale)
    {
        float delay = RhythmEngine.bitDelay - timeToScale;
        if (delay > 0)
        {
            yield return new WaitForSeconds(delay);
        }
        animator.speed = 1 / animationTime;

        animator.SetTrigger("Scale");

    }
    private IEnumerator ScaleOverTime(float scaleIncreaseFactor, float timeToScale)
    {
        float delay = RhythmEngine.bitDelay - timeToScale;

        if (delay > 0)
        {
            yield return new WaitForSeconds(delay);
        }

        float elapsedTime = 0;
        Vector3 startingScale = transform.localScale;
        Vector3 targetScale = new Vector3(startingScale.x * scaleIncreaseFactor, startingScale.y * scaleIncreaseFactor, startingScale.z * scaleIncreaseFactor);

        while (elapsedTime < timeToScale)
        {
            transform.localScale = Vector3.Lerp(startingScale, targetScale, elapsedTime / timeToScale);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = startingScale;
    }
}
