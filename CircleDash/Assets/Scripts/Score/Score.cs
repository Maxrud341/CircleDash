using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI  scoreText;
    public Animator animator;
    public int score = 0;
    private void Start() {
        scoreText = GetComponent<TextMeshProUGUI>();
        animator = GetComponent<Animator>();
    }
    public void EditScore(int hitScore){
        score += hitScore;
        scoreText.text = score.ToString();
        animator.SetTrigger("Get Score");
    }
}
