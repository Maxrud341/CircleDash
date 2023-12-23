using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI  scoreText;
    public int score = 0;
    private void Start() {
        scoreText = GetComponent<TextMeshProUGUI>();
    }
    public void EditScore(int hitScore){
        score += hitScore;
        scoreText.text = score.ToString();
    }
}
