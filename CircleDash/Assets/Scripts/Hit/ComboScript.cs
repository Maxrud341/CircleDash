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


    public void AddCombo(){
        if(combo==19)
        {
            miss.OnUnMiss();
        }
        if(combo<=19){
            combo++;
        }

        comboAnim.SetTrigger("trigger");
        comboText.text = combo.ToString() + "X";
    }

    public void ResetCombo(){
        combo = 1;
        comboText.text = combo.ToString() + "X";
    }
}
