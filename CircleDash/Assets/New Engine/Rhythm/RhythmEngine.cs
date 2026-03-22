using UnityEngine;
using System;

public class RhythmEngine : MonoBehaviour
{
    public float accuracy2;
    [SerializeField] private float steps;
    [SerializeField] public float bpm;
    [SerializeField] private float levelDuration;
    [SerializeField] private float onBitAccuracy;
    [SerializeField] private float latency = 0f; // в секундах, + если игрок жмёт поздно, - если рано
    [SerializeField] private AudioSource audioSource;
    [SerializeField] public AudioClip songClip;


    public static float accuracy;
    public static bool boolOnBit;
    public static float bitDelay;

    public static event Action OnBitEvent;
    public static event Action OnBetweenBit;
    public static event Action OnSongEndEvent;

    private bool songEnded = false;

    private int lastInterval;
    private int lastMidInterval;

    private float SongTime => (float)audioSource.timeSamples / audioSource.clip.frequency - latency;
    private bool IsPlaying => audioSource.isPlaying;

    public void StartGame()
    {
        bitDelay = 60f / bpm;
        songClip.LoadAudioData();
        audioSource.clip = songClip;
        audioSource.Play();
    }

    private void Update()
    {
        if (!IsPlaying) return;

        float intervalLength = GetIntervalLength(bpm);
        float sampledTime = SongTime / intervalLength;

        CheckForNewInterval(sampledTime);
        CheckForNewMidInterval(sampledTime * 2f);

        accuracy = GetAccuracy(sampledTime);
        accuracy2 = accuracy;
        boolOnBit = accuracy >= 1f - onBitAccuracy;

        if (!songEnded && SongTime >= levelDuration)
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
        int current = Mathf.FloorToInt(interval);
        if (current != lastInterval)
        {
            lastInterval = current;
            OnBitEvent?.Invoke();
        }
    }

    public void CheckForNewMidInterval(float interval)
    {
        int midInterval = Mathf.FloorToInt(interval);
        if (midInterval != lastMidInterval)
        {
            lastMidInterval = midInterval;
            if (midInterval % 2 == 1)
            {
                OnBetweenBit?.Invoke();
            }
        }
    }
}