using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ComboScript : MonoBehaviour
{
    public int combo = 0;
    public Animator comboAnim;
    public TextMeshProUGUI comboText;
    public Miss miss;
    public GameObject arrows;
    public ParticleSystem particleSystem1;
    public ParticleSystem particleSystem2;




    public void AddCombo(){
        if(combo==19)
        {
            particleSystem1.Play();
            particleSystem2.Play();
            miss.OnUnMiss();
        }
        if(combo<=19){
            combo++;
        }

        // comboAnim.SetTrigger("trigger");
        comboText.text = combo.ToString() + "X";
    }

    public void ResetCombo(){
        particleSystem1.Stop();
        particleSystem2.Stop();
        combo = 1;
        comboText.text = combo.ToString() + "X";
    }
}
