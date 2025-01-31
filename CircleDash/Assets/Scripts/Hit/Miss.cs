using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miss : MonoBehaviour
{
    public Animator[] animatorsX;
    public int num = 0;
    public Pause pause;
    public bool TemporaryImmortal;
    public AudioSource UnsuccessFX;
    public AudioSource UnMissFX;
    public ComboScript comboScript;
    public End end;

    public void OnMiss()
    {
        if (!TemporaryImmortal)
        {
            end.misses++;
            comboScript.ResetCombo();
            UnsuccessFX.Play();
            if (num < 0) num = 0;
            animatorsX[num].SetBool("Active", true);
            num++;
            StartCoroutine(TemporaryImmortality());
            if (num == 4)
            {
                Lose();
            }
        }

    }

    public void OnUnMiss()
    {
        UnMissFX.Play();
        switch (num)
        {

            case 1:
                animatorsX[0].SetBool("Active", false);
                break;
            case 2:
                animatorsX[1].SetBool("Active", false);
                break;
            case 3:
                animatorsX[2].SetBool("Active", false);
                break;
            default:
                break;
        }
        num--;
    }

    public void Lose()
    {
        pause.PauseGame();
        pause.OpenLoseCanv();
        pause.CloseMenuIco();
        pause.PauseMusic();
    }

    IEnumerator TemporaryImmortality()
    {
        TemporaryImmortal = true;
        yield return new WaitForSeconds(3);
        TemporaryImmortal = false;
    }
}
