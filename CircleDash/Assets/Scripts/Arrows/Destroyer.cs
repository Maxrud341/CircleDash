using System.Collections;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public ManageRangeArrow2Trap manageRangeArrow2Trap;
    public GameObject arrowsGenerator;
    public PlatesReaction platesReaction;

    public GameObject currentArrow;
    public int direction;

    private void FixedUpdate()
    {
        if (arrowsGenerator.transform.childCount == 0)
            platesReaction.resetPlates();
    }

    private void Start()
    {
        StartCoroutine(WaitForArrowsChangeDirection());
    }

    private IEnumerator WaitForArrowsChangeDirection()
    {
        while (true)
        {
            if (arrowsGenerator.transform.childCount > 0)
            {
                currentArrow = arrowsGenerator.transform.GetChild(0).gameObject;
                manageRangeArrow2Trap.arrow = currentArrow;
                float rotationZ = currentArrow.transform.rotation.eulerAngles.z;
                direction = GetDirectionFromRotation(rotationZ);
                platesReaction.switchPlate(direction);
                yield return StartCoroutine(WaitForArrowDestroy());
            }

            yield return null;
        }
    }

    private IEnumerator WaitForArrowDestroy()
    {
        while (currentArrow != null)
        {
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject);
        currentArrow = null;
    }

    private int GetDirectionFromRotation(float rotationZ)
    {
        switch ((int)rotationZ)
        {
            case 0:
                return 1;
            case 270:
                return 2;
            case 180:
                return 3;
            case 90:
                return 4;
            default:
                return -1;
        }
    }
}
