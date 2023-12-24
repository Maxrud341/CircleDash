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
            switch (direction)
            {
                case 1:
                    plateAnimators[0].SetTrigger("Plate Hitted");
                    platesParticles[0].startColor = platesReaction.plateColor;
                    platesParticles[0].Play();
                    break;
                case 2:
                    plateAnimators[1].SetTrigger("Plate Hitted");
                    platesParticles[1].startColor = platesReaction.plateColor;
                    platesParticles[1].Play();
                    break;
                case 3:
                    plateAnimators[2].SetTrigger("Plate Hitted");
                    platesParticles[2].startColor = platesReaction.plateColor;
                    platesParticles[2].Play();
                    break;
                case 4:
                    plateAnimators[3].SetTrigger("Plate Hitted");
                    platesParticles[3].startColor = platesReaction.plateColor;
                    platesParticles[3].Play();
                    break;
                default:
                    Debug.Log("ERROR DIRECTION ARROW");
                    break;
            }
        }
    }

}
