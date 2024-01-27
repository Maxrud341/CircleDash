using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeManager : MonoBehaviour
{
    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPosition;

    [SerializeField]
    private float minSwipeDistance = 100;

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
        if (Input.GetMouseButtonDown(0))
        {
            fingerDownPosition = Input.mousePosition;
            fingerUpPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            fingerUpPosition = Input.mousePosition;
            CheckSwipe();
        }
    }

    private void CheckSwipe()
{
    Vector2 swipeDirection = fingerUpPosition - fingerDownPosition;

    if (swipeDirection.magnitude > minSwipeDistance)
    {
        SwipeDirection direction;

        if (Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
            direction = (swipeDirection.x > 0) ? SwipeDirection.Right : SwipeDirection.Left;
        else
            direction = (swipeDirection.y > 0) ? SwipeDirection.Up : SwipeDirection.Down;

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
