using UnityEngine;
using TMPro;

public class Loading : MonoBehaviour
{
    public TextMeshProUGUI loadingText;

    private float timer;
    private int dots;

    void Update()
    {
        timer += Time.unscaledDeltaTime;
        if (timer >= 0.5f)
        {
            timer = 0;
            dots = (dots + 1) % 4;
            loadingText.text = "Analysing Song" + new string('.', dots);
        }
    }
}