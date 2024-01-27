using UnityEngine;

public class Joystick3 : MonoBehaviour
{
    private Vector2 mouseDownPosition;
    private Vector2 mouseUpPosition;
    public DetectHit detectHit;

    public bool detectSwipeOnlyAfterRelease = false;
    public float minSwipeDistance = 3f;

    void Update()
    {
        DetectSwipe();
    }

    void DetectSwipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseDownPosition = Input.mousePosition;
            mouseUpPosition = Input.mousePosition;
        }

        if (!detectSwipeOnlyAfterRelease && Input.GetMouseButton(0))
        {
            mouseUpPosition = Input.mousePosition;
            CheckSwipe();
        }

        if (Input.GetMouseButtonUp(0))
        {
            mouseUpPosition = Input.mousePosition;
            CheckSwipe();
        }
    }

    void CheckSwipe()
    {
        float swipeDistance = Vector2.Distance(mouseDownPosition, mouseUpPosition);

        if (swipeDistance > minSwipeDistance)
        {
            Vector2 swipeDirection = mouseUpPosition - mouseDownPosition;
            swipeDirection.Normalize();

            if (Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
            {
                if (swipeDirection.x > 0)
                {
                    detectHit.tryDirection(2);
                }
                else
                {
                    detectHit.tryDirection(4);
                }
            }
            else
            {
                if (swipeDirection.y > 0)
                {
                    detectHit.tryDirection(1);
                }
                else
                {
                    detectHit.tryDirection(3);
                }
            }
        }
    }
}
