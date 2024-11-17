using System;
using System.Collections;
using UnityEngine;

public class HitManager : MonoBehaviour
{
    public GameObject currentArrow;
    public Animator[] plateAnimators;
    public ParticleSystem[] PlatePS;
    public ParticleSystem ArrowPS;

    public AudioSource SuccessAS;
    public AudioClip Success1;
    public AudioClip Success2;

    public AudioSource UnSuccessAS;
    private int num = 0;


    public Animator circle;
    public Animator bg;
    public float missCooldown = 3f;
    private bool isOnCooldown = false;

    public void OnSuccessHit(int direction, int score, GameObject arrow)
    {
        circle.SetTrigger(direction.ToString());
        bg.SetTrigger(direction.ToString());
        plateAnimators[direction - 1].SetTrigger("Plate Hitted");



        currentArrow.GetComponent<ArrowProperties>().DestroySelf();
        currentArrow = null;


        Score.AddScore((int)Math.Floor(score * Combo.comboMultiplier));
        Combo.AddCombo();

        Color currentColor = arrow.GetComponent<ArrowProperties>().rhythmColorTransition.currentColor;
        PlatePS[direction - 1].startColor = currentColor;
        PlatePS[direction - 1].Play();
        ArrowPS.startColor = currentColor;
        ArrowPS.Play();
        // if(num % 2 == 0){
        //     SuccessAS.clip = Success1;
        // } else {
        //     SuccessAS.clip = Success2;  
        // }
        // num++;
        SuccessAS.Play();
    }

    public void OnUnsuccessHit(int direction)
    {
        circle.SetTrigger(direction.ToString());
        bg.SetTrigger(direction.ToString());
        plateAnimators[direction - 1].SetTrigger("Plate Hitted");



        PlatePS[direction - 1].startColor = Color.red;
        PlatePS[direction - 1].Play();
        ArrowPS.startColor = Color.red;
        ArrowPS.Play();

        Miss();

    }

    void OnArrowSkip()
    {
        Debug.Log("Skip arrow");
        Miss();

    }
    void Miss()
    {
        if (isOnCooldown)
        {
            Debug.Log("Miss is on cooldown!");
            return;
        }

        UnSuccessAS.Play();
        Combo.ResetCombo();
        Lives.RemoveLife();

        StartCoroutine(MissCooldownCoroutine());
    }

    private IEnumerator MissCooldownCoroutine()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(missCooldown);
        isOnCooldown = false;
    }

    private void OnEnable()
    {
        RhythmJoystick.OnHitDetected += OnHitDetected;
        CurrentArrowManager.OnNewCurrentArrow += OnNewCurrentArrow;
        ArrowProperties.OnArrowSkip += OnArrowSkip;
    }

    private void OnDisable()
    {
        RhythmJoystick.OnHitDetected -= OnHitDetected;
        CurrentArrowManager.OnNewCurrentArrow -= OnNewCurrentArrow;
        ArrowProperties.OnArrowSkip -= OnArrowSkip;

    }

    private void OnHitDetected(int direction)
    {
        CalculateHit(direction, currentArrow);
    }

    private void OnNewCurrentArrow(GameObject arrow)
    {
        currentArrow = arrow;
    }

    void CalculateHit(int direction, GameObject arrow)
    {
        if (currentArrow != null && direction == arrow.GetComponent<ArrowProperties>().direction && arrow.GetComponent<ArrowProperties>().canBeHitted)
        {
            int hitScore = (int)Math.Floor(RhythmEngine.accuracy * 100f);
            OnSuccessHit(direction, hitScore, arrow);
            Debug.Log("Score:" + hitScore);
        }
        else
        {
            Debug.Log("Miss");
            OnUnsuccessHit(direction);
        }


    }


}
