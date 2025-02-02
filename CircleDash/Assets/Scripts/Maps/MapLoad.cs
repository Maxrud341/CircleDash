using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapLoad : MonoBehaviour
{
    public int mapIndex;
    public void loadMap()
    {
        FadeIn();
        StartCoroutine(LoadSceneAfterFade(mapIndex));
    }

    private IEnumerator LoadSceneAfterFade(int mapIndex)
    {
        yield return new WaitForSecondsRealtime(0.6f);
        SceneManager.LoadScene(mapIndex);
    }

    public Animator fadeBlack;
    void Start()
    {
        fadeBlack.updateMode = AnimatorUpdateMode.UnscaledTime;
        fadeBlack.SetTrigger("StartOut");
    }

    public void FadeIn()
    {
        fadeBlack.SetTrigger("IN");
    }
}
