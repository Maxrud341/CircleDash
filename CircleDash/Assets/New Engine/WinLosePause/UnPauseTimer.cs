using UnityEngine;
using TMPro;
using System.Collections;

public class UnPauseTimer : MonoBehaviour
{
    public TextMeshProUGUI text;
    public RhythmJoystick joystick;
    public PauseManager pauseManager;
    public AudioSource audioSource;

    void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        for (int i = 3; i >= 1; i--)
        {
            yield return StartCoroutine(AnimateNumber(i.ToString()));
        }

        yield return StartCoroutine(AnimateNumber("GO!"));

        pauseManager.OpenMenuIco();
        pauseManager.MusicUnPause();
        pauseManager.UnPauseGame();


        gameObject.SetActive(false);
    }

    IEnumerator AnimateNumber(string number)
    {
        text.text = number;
        audioSource.Play();

        float duration = 0.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = elapsed / duration;

            float scale = Mathf.Lerp(1.8f, 1f, Mathf.Pow(t, 0.3f));
            text.transform.localScale = Vector3.one * scale;

            float alpha = t < 0.7f ? 1f : Mathf.Lerp(1f, 0f, (t - 0.7f) / 0.3f);
            text.color = new Color(1f, 1f, 1f, alpha);

            yield return null;
        }
    }
}