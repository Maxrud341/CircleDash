using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class End : MonoBehaviour
{
    public Pause pause;
    public int score;
    public TextMeshProUGUI scoreText;
    public Score scoreScript;
    public Joystick3 joystick3;

    public int misses;
    public int greenArrow;
    public int orangeArrow;

    public TextMeshProUGUI missesTM;
    public TextMeshProUGUI greenArrowTM;
    public TextMeshProUGUI orangeArrowTM;
    public void EndMap(){
        score = scoreScript.score;
        pause.OpenEndMenu();
        pause.CloseMenuIco();
        joystick3.enabled = false;
        scoreText.text = score.ToString();

        missesTM.text = " - " + misses.ToString();
        greenArrowTM.text = " - " + greenArrow.ToString();
        orangeArrowTM.text = " - " + orangeArrow.ToString();
    }
}
