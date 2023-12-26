using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class fps : MonoBehaviour
{
    public TextMeshProUGUI text;
    public int fpss;
    private void Update() {
        fpss = (int)( 1.0f / Time.deltaTime);
        text.text = fpss.ToString();
    }
}
