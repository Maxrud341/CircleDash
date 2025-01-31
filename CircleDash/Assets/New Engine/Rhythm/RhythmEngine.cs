using UnityEngine;
using System.Collections;
using System;


public class RhythmEngine : MonoBehaviour
{
    public float accuracy2;
    [SerializeField] private float steps;
    [SerializeField] private float bpm;
    [SerializeField] private float levelDuration;
    [SerializeField] private float onBitAccuracy;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip songClip;



    public static float accuracy;
    public static bool boolOnBit;
    public static float bitDelay;

    public static event Action OnBitEvent;
    public static event Action OnBetweenBit;
    public static event Action OnSongEndEvent;

    private float levelStartTime;
    private bool songEnded = false;

    int num = 1;



    void Awake()
    {
        //bpm = UniBpmAnalyzer.AnalyzeBpm(songClip);

        bitDelay = 60f / bpm;
        //audioSource.time = 40;
        audioSource.clip = songClip;

        levelStartTime = Time.time;
        //StartCoroutine(PlayAudioWithDelay(1f));
        audioSource.Play();
    }

    IEnumerator PlayAudioWithDelay(float delay)
    {

        yield return new WaitForSeconds(delay);
        audioSource.Play();

    }

    private int lastInterval;
    private int lastMidInterval;

    private void FixedUpdate()
    {
        float sampledTime = audioSource.timeSamples / (audioSource.clip.frequency * GetIntervalLength(bpm));

        CheckForNewInterval(sampledTime - 0.15f);
        CheckForNewMidInterval(sampledTime * 2f - 0.15f);

        accuracy = GetAccuracy(sampledTime);
        accuracy2 = accuracy;
        boolOnBit = (accuracy >= 1 - onBitAccuracy);

        if ((!audioSource.isPlaying || Time.time - levelStartTime >= levelDuration) && !songEnded)
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

