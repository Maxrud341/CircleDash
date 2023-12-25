using UnityEngine;

public class Joystick2 : MonoBehaviour
{
    [SerializeField] private DetectHit detectHit;
    [SerializeField] private float maxDistance = 4f;

    private Vector2 initialObjectPosition2D;
    private Vector2 lastMousePosition;
    private bool mouseMoved = false;

    void Start()
    {
        initialObjectPosition2D = transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = mousePosition - initialObjectPosition2D;
            float distance = direction.magnitude;

            if (distance > maxDistance)
            {
                direction = direction.normalized * maxDistance;
                transform.position = initialObjectPosition2D + direction;
            }
            else
            {
                transform.position = mousePosition;
            }

            if (lastMousePosition != mousePosition)
            {
                mouseMoved = true;
            }

            lastMousePosition = mousePosition;
        }
        else
        {
            transform.position = initialObjectPosition2D; // Устанавливаем позицию в начальную точку, когда кнопка мыши отпущена

            if (mouseMoved)
            {
                DetermineMouseDirection(); // Вызываем метод только если мышь действительно двигалась
                mouseMoved = false; // Сбрасываем флаг после вызова метода
            }
        }
    }

    void DetermineMouseDirection()
    {
        Vector2 direction = lastMousePosition - initialObjectPosition2D;

        if (Mathf.Abs(direction.y) > Mathf.Abs(direction.x))
        {
            if (direction.y > 0)
            {
                detectHit.tryDirection(1);
            }
            else if (direction.y < 0)
            {
                detectHit.tryDirection(3);
            }
        }
        else
        {
            if (direction.x > 0)
            {
                detectHit.tryDirection(2);
            }
            else if (direction.x < 0)
            {
                detectHit.tryDirection(4);
            }
        }
    }
}
