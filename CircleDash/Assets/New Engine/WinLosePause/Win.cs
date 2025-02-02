using UnityEngine;
using TMPro;
using System.Collections;
using Unity.VisualScripting; // Не забудь добавить этот using

public class Win : MonoBehaviour
{
    public WinSave winSave;
    public PauseManager pause;
    public Animator fadeBlack;

    // Grades
    public GradesManager gradesManager;
    public int PerfectGradeCount = 0;
    public int GoodGradeCount = 0;
    public int MissGradeCount = 0;

    public string Grade;
    public TextMeshProUGUI GradeText;

    public TextMeshProUGUI PerfectGradeCountText;
    public TextMeshProUGUI GoodGradeCountText;
    public TextMeshProUGUI MissGradeCountText;

    // Score
    private int score;
    public TextMeshProUGUI scoreText;
    void OnEnable()
    {
        RhythmEngine.OnSongEndEvent += StartWinSequence;
    }

    void OnDisable()
    {
        RhythmEngine.OnSongEndEvent -= StartWinSequence;
    }

    void StartWinSequence()
    {
        StartCoroutine(WinWithDelay());
    }

    IEnumerator WinWithDelay()
    {
        yield return new WaitForSeconds(RhythmEngine.bitDelay * 6);

        fadeBlack.SetTrigger("IN");

        yield return new WaitForSeconds(0.1f);
        yield return new WaitUntil(() => fadeBlack.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);

        OnWin();

        fadeBlack.SetTrigger("OUT");
    }
    void OnWin()
    {
        //pause.PauseGame();
        pause.OpenWinCanv();
        pause.CloseMenuIco();

        GetGradesCount();
        GetScore();
        Grade = GetGrade(PerfectGradeCount, GoodGradeCount, MissGradeCount);


        ApplyGradesCount();
        ApplyScore();
        ApplyGrade();

        winSave.SaveWinData();
    }

    void GetGradesCount()
    {
        PerfectGradeCount = gradesManager.PerfectGradeCount;
        GoodGradeCount = gradesManager.GoodGradeCount;
        MissGradeCount = gradesManager.MissGradeCount;
    }

    void GetScore()
    {
        score = Score.score;
    }

    void ApplyGradesCount()
    {
        PerfectGradeCountText.text = " - " + PerfectGradeCount.ToString();
        GoodGradeCountText.text = " - " + GoodGradeCount.ToString();
        MissGradeCountText.text = " - " + MissGradeCount.ToString();
    }

    void ApplyScore()
    {
        scoreText.text = score.ToString();
    }

    void ApplyGrade()
    {
        GradeText.text = Grade;
    }

    string GetGrade(int perfect, int good, int miss)
    {
        int total = perfect + good + miss;
        if (total == 0) return "D";

        float accuracy = (perfect + 0.6f * good) / total * 100;

        if (perfect == total) return "S";
        if (accuracy >= 90 && miss == 0) return "A";
        if (accuracy >= 80) return "B";
        if (accuracy >= 50) return "C";
        return "D";
    }
}
