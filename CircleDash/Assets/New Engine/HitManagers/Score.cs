using TMPro;
using System.Collections;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static TextMeshProUGUI scoreText; // Текст для отображения счета
    public static int score = 0;            // Текущий счет
    private int displayedScore = 0;         // Отображаемый счет
    public int stepSize = 15;               // Шаг увеличения (можно настроить)

    private void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        score = 0;
        scoreText.text = "0";
    }

    public static void AddScore(int hitScore)
    {
        score += hitScore;
        if (scoreText != null)
        {
            scoreText.GetComponent<Score>().StartCoroutine(scoreText.GetComponent<Score>().AnimateScoreChange());
        }
    }

    private IEnumerator AnimateScoreChange()
    {

        while (displayedScore < score)
        {
            displayedScore += stepSize; 
            if (displayedScore > score) 
            {
                displayedScore = score;
            }

            UpdateScoreDisplay();
            yield return new WaitForSeconds(0.025f);
        }
    }

    private void UpdateScoreDisplay()
    {
        scoreText.text = displayedScore.ToString();
    }
}