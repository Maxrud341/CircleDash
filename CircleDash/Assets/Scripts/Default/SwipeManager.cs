using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeManager : MonoBehaviour
{
    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPosition;

    [SerializeField]
    private float minSwipeDistance = 5f;

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
