using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMove : MonoBehaviour
{
    public float speed = 5.0f;

    void Update()
    {
        // Движение влево
        Vector3 movement = Vector3.left * speed * Time.deltaTime;

        // Перемещаем объект
        transform.Translate(movement);
    }
}
