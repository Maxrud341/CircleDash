using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeTest : MonoBehaviour
{
    void Start()
    {
        SwipeManager.SwipeDetected += HandleSwipe;
    }


    void OnDisable()
    {
        SwipeManager.SwipeDetected -= HandleSwipe;
    }

    private void HandleSwipe(SwipeManager.SwipeDirection direction)
    {
        switch (direction)
        {
            case SwipeManager.SwipeDirection.Up:
                Debug.Log("Swipe up detected.");
                // Обработка свайпа вверх
                break;
            case SwipeManager.SwipeDirection.Down:
                Debug.Log("Swipe down detected.");
                // Обработка свайпа вниз
                break;
            case SwipeManager.SwipeDirection.Left:
                Debug.Log("Swipe left detected.");
                // Обработка свайпа влево
                break;
            case SwipeManager.SwipeDirection.Right:
                Debug.Log("Swipe right detected.");
                // Обработка свайпа вправо
                break;
        }
    } 
}
