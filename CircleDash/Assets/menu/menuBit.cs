using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuBit : MonoBehaviour
{
    public int bpm;
    public float bitDelay;
    public Animator[] animators;
    private void Start() {
        bitDelay = 60f / bpm;
        StartCoroutine(SpawnArrowsWithDelay());
    }

    private IEnumerator SpawnArrowsWithDelay()
    {
        while (true){
            yield return new WaitForSeconds(bitDelay);
            foreach (Animator i in animators)
            {
                i.SetTrigger("a");      
            }
        }
       
            
    }
}
