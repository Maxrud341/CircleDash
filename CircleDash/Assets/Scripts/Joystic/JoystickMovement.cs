using UnityEngine;

public class JoystickMovement : MonoBehaviour
{
    [SerializeField]
    private float maxDistance = 4f;

    private Vector3 initialObjectPosition3D;
    private Vector2 initialObjectPosition2D;
    private bool isMousePressed = false;

    public DetectHit detectHit;

    void Start()
    {
        initialObjectPosition2D = transform.position;
        initialObjectPosition3D = transform.position;

        SwipeManager.SwipeDetected += HandleSwipe;
    }

    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            isMousePressed = true;

            Vector2 mousePosition = Input.mousePosition;
            Vector2 targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);


            float distance = Vector2.Distance(targetPosition, initialObjectPosition2D);
            if (distance <= maxDistance)
            {
 
                

                transform.position = new Vector3(targetPosition.x, targetPosition.y, transform.position.z);
            }
            else
            {
                Vector3 direction = (targetPosition - initialObjectPosition2D).normalized;
                transform.position = initialObjectPosition3D + direction * maxDistance;
            }
        }
        else if (isMousePressed)
        {
            isMousePressed = false;
            transform.position = initialObjectPosition3D;
        }
    }

    private void HandleSwipe(SwipeManager.SwipeDirection direction)
    {
        switch (direction)
        {
            case SwipeManager.SwipeDirection.Up:
                Debug.Log("Swipe up detected.");
                detectHit.tryDirection(1);
                break;
            case SwipeManager.SwipeDirection.Right:
                Debug.Log("Swipe down detected.");
                detectHit.tryDirection(2);
                break;
            case SwipeManager.SwipeDirection.Down:
                Debug.Log("Swipe left detected.");
                detectHit.tryDirection(3);
                break;
            case SwipeManager.SwipeDirection.Left:
                Debug.Log("Swipe right detected.");
                detectHit.tryDirection(4);
                break;
        }
    } 
}
