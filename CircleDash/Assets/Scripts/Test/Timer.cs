using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private float startTime;
    private float elapsedTime;
    private bool isRunning = false;


    void Update()
    {
        if (isRunning)
        {
            elapsedTime = Time.time - startTime;
        }
    }

    public void StartTimer()
    {
        startTime = Time.time - elapsedTime;
        isRunning = true;
    }

    public void StopTimer()
    {
        isRunning = false;
        Debug.Log(elapsedTime);
    }
}
