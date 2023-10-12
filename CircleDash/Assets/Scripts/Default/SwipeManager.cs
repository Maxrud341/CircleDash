using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeManager : MonoBehaviour
{
    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPosition;

    [SerializeField]
    private bool detectSwipeOnlyAfterRelease = false;

    [SerializeField]
    private float minSwipeDistance = 20f;

    // Event to be triggered when a swipe is detected
    public delegate void OnSwipeDetected(SwipeDirection direction);
    public static event OnSwipeDetected SwipeDetected;

    // Possible swipe directions
    public enum SwipeDirection
    {
        Up,
        Down,
        Left,
        Right
    }

    void Update()
    {
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                fingerDownPosition = touch.position;
                fingerUpPosition = touch.position;
            }

            if (!detectSwipeOnlyAfterRelease && touch.phase == TouchPhase.Moved)
            {
                fingerUpPosition = touch.position;
                CheckSwipe();
            }

            if (touch.phase == TouchPhase.Ended)
            {
                fingerUpPosition = touch.position;
                CheckSwipe();
            }
        }
    }

    private void CheckSwipe()
    {
        if (IsVerticalSwipe())
        {
            SwipeDirection direction = (fingerUpPosition.y - fingerDownPosition.y > 0) ? SwipeDirection.Up : SwipeDirection.Down;
            SwipeDetected?.Invoke(direction);
        }
        else if (IsHorizontalSwipe())
        {
            SwipeDirection direction = (fingerUpPosition.x - fingerDownPosition.x > 0) ? SwipeDirection.Right : SwipeDirection.Left;
            SwipeDetected?.Invoke(direction);
        }
    }

    private bool IsVerticalSwipe()
    {
        return Mathf.Abs(fingerUpPosition.y - fingerDownPosition.y) > minSwipeDistance;
    }

    private bool IsHorizontalSwipe()
    {
        return Mathf.Abs(fingerUpPosition.x - fingerDownPosition.x) > minSwipeDistance;
    }
}
