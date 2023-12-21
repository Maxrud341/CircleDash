using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageRangeArrow2Trap : MonoBehaviour
{
    public GameObject arrow;
    public GameObject trap;
    public PlatesReaction platesReaction;
    void Update()
    {
        if (arrow != null)
        {
            float distance = Vector2.Distance(arrow.transform.position, trap.transform.position);
            float normalizedDistance = Mathf.Clamp01(distance / 2.5f);
            platesReaction.transitionValue = normalizedDistance;
        }
    }
}
