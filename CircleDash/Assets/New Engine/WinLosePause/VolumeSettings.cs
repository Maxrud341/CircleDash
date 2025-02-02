using UnityEngine;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    public AudioSource[] musicAS;
    public float volumeLvl;
    private Scrollbar scrollbar;
    void Start()
    {
        scrollbar = GetComponent<Scrollbar>();

        scrollbar.onValueChanged.AddListener(OnScrollbarChanged);
        ChangeScrollbarValue();
    }



    void OnScrollbarChanged(float value)
    {
        volumeLvl = value;
        ChangeVolume();
    }


    public void ChangeVolume()
    {
        foreach (AudioSource audioSource in musicAS)
        {
            audioSource.volume = volumeLvl;
        }
    }

    public void ChangeScrollbarValue()
    {
        scrollbar.value = volumeLvl;  
    }
}
