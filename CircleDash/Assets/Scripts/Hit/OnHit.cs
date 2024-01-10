using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHit : MonoBehaviour
{

    public Score score;
    public PlatesReaction platesReaction;
    public AudioSource SuccessFX;
    public AudioSource UnsuccessFX;

    public ParticleSystem[] platesParticles;
    public ParticleSystem arrowHit;
    public Animator[] plateAnimators;
    public Animator circle;
    public Animator bg;


    public void OnSuccessHit(int direction, int hitScore)
    {
        score.EditScore(hitScore);


        SuccessFX.Play();


        circle.SetTrigger(direction.ToString());

        bg.SetTrigger(direction.ToString());

        plateAnimators[direction-1].SetTrigger("Plate Hitted");

        platesParticles[direction-1].startColor = platesReaction.plateColor;
        platesParticles[direction-1].Play();

        arrowHit.startColor = platesReaction.plateColor;
        arrowHit.Play();

        arrowHit.startColor = platesReaction.plateColor;
        



    }

    public void OnUnsuccessHit(int direction, int hitScore){

        UnsuccessFX.Play();

        circle.SetTrigger(direction.ToString());

        bg.SetTrigger(direction.ToString());

        plateAnimators[direction-1].SetTrigger("Plate Hitted");

        platesParticles[direction-1].startColor = Color.red;
        platesParticles[direction-1].Play();

        arrowHit.startColor = Color.red;
        arrowHit.Play();

        arrowHit.startColor = Color.red;   
    }
}
