using UnityEngine;
using System.Collections.Generic;

public class MusicAnalyser : MonoBehaviour
{
    public int sampleSize = 1024; // Размер буфера для анализа
    public float sensitivity = 1.5f; // Чувствительность к ударам
    public float analysisInterval = 0.1f; // Интервал анализа (в секундах)
    public int minHitsForIntensity = 5; // Минимальное количество ударов для секции

    private List<float> hitTimes = new List<float>(); // Временные метки ударов
    private List<Vector2> intenseSections = new List<Vector2>(); // Участки с высокой интенсивностью
    private int bpm; // Рассчитанный BPM

    /// <summary>
    /// Анализирует аудиоклип и заполняет BPM и участки интенсивности.
    /// </summary>
    public void AnalyseAudio(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.LogError("AudioClip is null! Please provide a valid clip.");
            return;
        }

        // Подготовка данных
        int totalSamples = clip.samples;
        int sampleRate = clip.frequency;
        int totalSteps = Mathf.CeilToInt((float)totalSamples / (analysisInterval * sampleRate));

        float[] audioData = new float[totalSamples];
        clip.GetData(audioData, 0);

        hitTimes.Clear();
        intenseSections.Clear();

        // Анализируем каждую часть аудиофайла
        for (int step = 0; step < totalSteps; step++)
        {
            int startSample = Mathf.Min(step * (int)(analysisInterval * sampleRate), totalSamples - sampleSize);
            float[] samples = new float[sampleSize];
            System.Array.Copy(audioData, startSample, samples, 0, sampleSize);

            float intensity = CalculateIntensity(samples);

            if (intensity > sensitivity)
            {
                hitTimes.Add(step * analysisInterval);
            }
        }

        // Рассчитываем BPM
        bpm = CalculateBPM(hitTimes);

        // Определяем участки интенсивности
        intenseSections = GroupIntenseSections(hitTimes);

        Debug.Log($"Analysis complete. BPM: {bpm}, Intense Sections: {intenseSections.Count}");
    }

    /// <summary>
    /// Возвращает BPM трека.
    /// </summary>
    public int GetBPM()
    {
        return bpm;
    }

    /// <summary>
    /// Возвращает временные участки высокой интенсивности.
    /// </summary>
    public List<Vector2> GetIntenseSections()
    {
        return intenseSections;
    }

    /// <summary>
    /// Вычисляет интенсивность для набора сэмплов.
    /// </summary>
    private float CalculateIntensity(float[] samples)
    {
        float sum = 0f;
        foreach (var sample in samples)
        {
            sum += sample * sample; // Вычисление энергии сигнала
        }
        return sum / samples.Length;
    }

    /// <summary>
    /// Рассчитывает BPM по временным меткам ударов.
    /// </summary>
    private int CalculateBPM(List<float> hitTimes)
    {
        if (hitTimes.Count < 2) return 0;

        List<float> intervals = new List<float>();
        for (int i = 1; i < hitTimes.Count; i++)
        {
            intervals.Add(hitTimes[i] - hitTimes[i - 1]);
        }

        float avgInterval = 0f;
        foreach (float interval in intervals)
        {
            avgInterval += interval;
        }
        avgInterval /= intervals.Count;

        return Mathf.RoundToInt(60f / avgInterval);
    }

    /// <summary>
    /// Определяет временные участки с высокой плотностью ударов.
    /// </summary>
    private List<Vector2> GroupIntenseSections(List<float> hitTimes)
    {
        List<Vector2> sections = new List<Vector2>();
        if (hitTimes.Count == 0) return sections;

        float sectionStart = hitTimes[0];
        int hitsInSection = 1;

        for (int i = 1; i < hitTimes.Count; i++)
        {
            if (hitTimes[i] - hitTimes[i - 1] <= analysisInterval)
            {
                hitsInSection++;
            }
            else
            {
                if (hitsInSection >= minHitsForIntensity)
                {
                    sections.Add(new Vector2(sectionStart, hitTimes[i - 1]));
                }
                sectionStart = hitTimes[i];
                hitsInSection = 1;
            }
        }

        if (hitsInSection >= minHitsForIntensity)
        {
            sections.Add(new Vector2(sectionStart, hitTimes[hitTimes.Count - 1]));
        }

        return sections;
    }
}
