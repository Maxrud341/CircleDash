using UnityEngine;

public class ApplySettings : MonoBehaviour
{
    public float musicVolume = 1f;
    public float sfxVolume = 1f;
    public float latency = 0f;

    public AudioSource[] musicAS;
    public AudioSource[] sfxAS;
    public RhythmEngine rhythmEngine;

    public void ApplyAllSettings()
    {
        // Apply music volume
        foreach (AudioSource audioSource in musicAS)
        {
            audioSource.volume = musicVolume;
        }

        // Apply SFX volume
        foreach (AudioSource audioSource in sfxAS)
        {
            audioSource.volume = sfxVolume;
        }

        // Apply latency
        rhythmEngine.latency = latency;

    }

    public void ApplyVolume(float newVolume, VolumeTypes type)
    {
        if (type == VolumeTypes.Music)
        {
            musicVolume = newVolume;
            foreach (AudioSource audioSource in musicAS)
            {
                audioSource.volume = musicVolume;
            }
        }
        else if (type == VolumeTypes.SFX)
        {
            sfxVolume = newVolume;
            foreach (AudioSource audioSource in sfxAS)
            {
                audioSource.volume = sfxVolume;
            }
        }
    }


    public void ApplyLatency(float newLatency)
    {
        latency = newLatency;
        rhythmEngine.latency = latency;
    }
}

