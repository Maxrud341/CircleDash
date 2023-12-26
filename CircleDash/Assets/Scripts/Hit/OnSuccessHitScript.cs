using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnSuccessHitScript : MonoBehaviour
{

    public Score score;
    public PlatesReaction platesReaction;
    public AudioSource audioSource;

    public ParticleSystem[] platesParticles;
    public ParticleSystem arrowHit;
    public Animator[] plateAnimators;
    public Animator circle;
    public Animator bg;


    public void OnSuccessHit(int direction, int hitScore)
    {
        score.EditScore(hitScore);


        audioSource.Play();


        circle.SetTrigger(direction.ToString());

        bg.SetTrigger(direction.ToString());

        plateAnimators[direction-1].SetTrigger("Plate Hitted");

        platesParticles[direction-1].startColor = platesReaction.plateColor;
        platesParticles[direction-1].Play();

        arrowHit.startColor = platesReaction.plateColor;
        arrowHit.Play();

        arrowHit.startColor = platesReaction.plateColor;
        



    }
}
