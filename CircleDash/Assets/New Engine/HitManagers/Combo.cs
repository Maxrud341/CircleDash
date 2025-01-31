using TMPro;
using UnityEngine;
using System;

public class Combo : MonoBehaviour
{
    public AudioSource audioSource;
    public static AudioSource audioSourceStatic;
    private static int combo = 1;
    public static float comboMultiplier = 1f;
    public static int maxCombo = 10;
    public static TextMeshProUGUI comboText;
    public static event Action OnMaxCombo;

    public ParticleSystem[] particleSystems;
    private static ParticleSystem[] particleSystemsStatic;



    private void Start()
    {
        comboText = GetComponent<TextMeshProUGUI>();
        particleSystemsStatic = particleSystems;
        audioSourceStatic = audioSource;
    }
    public static void AddCombo()
    {
        combo++;
        if (combo < maxCombo)
        {
            comboMultiplier += 0.2f;
            comboText.text = combo.ToString() + "X";
            // animator?.SetTrigger("trigger");
        }
        else if (combo == maxCombo)
        {
            foreach (ParticleSystem particle in particleSystemsStatic)
            {
                audioSourceStatic.Play();
                particle.Play();
            }
            comboText.text = maxCombo.ToString() + "X";
        }


        if (combo % 50 == 1) Lives.RestoreLife();
    }

    public static void ResetCombo()
    {
        foreach (ParticleSystem particle in particleSystemsStatic)
        {
            particle.Stop();
        }
        comboMultiplier = 1;
        combo = 1;
        comboText.text = combo.ToString() + "X";
    }

}

