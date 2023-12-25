using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectHit : MonoBehaviour
{
    public ManageRangeArrow2Trap manageRangeArrow2Trap;
    public Destroyer destroyer;
    public Score score;
    public PlatesReaction platesReaction;
    public int hitScore;
    public AudioSource audioSource;

    public ParticleSystem[] platesParticles;
    public Animator[] plateAnimators;


    public void tryDirection(int direction)
    {
        hitScore = (int)((1 - manageRangeArrow2Trap.normalizedDistance) * 100);
        if (hitScore > 70 && destroyer.direction == direction)
        {
            Debug.Log("SUCCESS!!");
            Debug.Log(hitScore);
            score.EditScore(hitScore);
            audioSource.Play();
            Destroy(destroyer.currentArrow);
            OnPlateHitted(direction);
        }
    }

    public void OnPlateHitted(int direction)
    {
        plateAnimators[direction-1].SetTrigger("Plate Hitted");
        platesParticles[direction-1].startColor = platesReaction.plateColor;
        platesParticles[direction-1].Play();
    }

}
