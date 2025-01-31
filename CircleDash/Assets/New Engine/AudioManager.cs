using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{


    void Start()
    {
        var config = AudioSettings.GetConfiguration();
        config.dspBufferSize = 1024;

        if (AudioSettings.Reset(config))
        {
            Debug.Log($"DSP Buffer Size успешно установлен на {config.dspBufferSize}");
        }
        else
        {
            Debug.LogWarning("Не удалось изменить DSP Buffer Size. Проверьте поддержку устройства.");
        }


    }
}
