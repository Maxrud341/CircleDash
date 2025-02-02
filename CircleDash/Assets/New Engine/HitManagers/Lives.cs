using UnityEngine;
using System;

public class Lives : MonoBehaviour
{
    public static int lives = 4;
    public static Animator[] livesAnimators;

    public static event Action OnLose;

    void Start()
    {
        livesAnimators = GetComponentsInChildren<Animator>();
        Array.Reverse(livesAnimators);
        UpdateLives();
        lives = 4;
    }

    public static void RemoveLife()
    {
        if (lives > 0)
        {
            lives--;
            UpdateAnimator(lives, true);

            if (lives == 0)
            {
                OnLose?.Invoke();
                Debug.Log("GG!");
            }
        }
    }

    public static void RestoreLife()
    {
        if (lives < livesAnimators.Length)
        {
            UpdateAnimator(lives, false);
            lives++;
        }
    }

    private static void UpdateAnimator(int index, bool isActive)
    {
        if (index >= 0 && index < livesAnimators.Length)
        {
            livesAnimators[index].SetBool("Active", isActive);
        }
    }

    private void UpdateLives()
    {
        for (int i = 0; i < livesAnimators.Length; i++)
        {
            livesAnimators[i].SetBool("Active", false);
        }
    }
}
