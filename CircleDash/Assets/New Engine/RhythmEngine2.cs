using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System;

public class RhythmEngine2 : MonoBehaviour
{
    [SerializeField] private float steps;
    [SerializeField] private float bpm;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip songClip;
    [SerializeField] private float onBitAccuracy;
    [SerializeField] public float bitDelay;
    private float songLength;
    [SerializeField, Range(0f, 1f)] public static float accuracy;
    [SerializeField] public static bool boolOnBit;

    public static event Action OnBitEvent;
    public static event Action OnSongEndEvent;

    private bool songEnded = true;


    private int lastInterval;

    void Awake()
    {
        bitDelay = 60f / bpm;
        audioSource.time = 40;
        audioSource.clip = songClip;
        songLength = songClip.length;
        StartCoroutine(PlayAudioWithDelay(2f));
    }

    IEnumerator PlayAudioWithDelay(float delay)
    {

        yield return new WaitForSeconds(delay);
        audioSource.Play();

    }

    private void Update()
    {

        float sampledTime = audioSource.timeSamples / (audioSource.clip.frequency * GetIntervalLength(bpm));
        CheckForNewInterval(sampledTime - 0.15f);

        accuracy = GetAccuracy(sampledTime);

        boolOnBit = (accuracy >= 1 - onBitAccuracy) ? true : false;

        if (!audioSource.isPlaying && !songEnded)
        {
            songEnded = true;
            OnSongEndEvent?.Invoke();
        }


    }

    private float GetAccuracy(float sampledTime)
    {
        float currentBit = Mathf.Floor(sampledTime);
        float nextBit = currentBit + 1f;

        float distanceToCurrentBit = Mathf.Abs(sampledTime - currentBit);
        float distanceToNextBit = Mathf.Abs(sampledTime - nextBit);

        float minDistance = Mathf.Min(distanceToCurrentBit, distanceToNextBit);

        return 1f - Mathf.Clamp01(minDistance / 0.5f);
    }

    public float GetIntervalLength(float bpm)
    {
        return 60f / (bpm * steps);
    }

    public void CheckForNewInterval(float interval)
    {
        if (Mathf.FloorToInt(interval) != lastInterval)
        {
            lastInterval = Mathf.FloorToInt(interval);
            OnBitEvent?.Invoke();
        }
    }
}



