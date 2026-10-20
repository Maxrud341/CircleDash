using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [Header("Map Settings")]
    public GameObject bg;
    public AudioClip songClip;
    public float bpm = 120f;
    public Color color = Color.white;
    public float levelDuration = 30f;
    public List<Vector2> X2_Sections = new List<Vector2>();

    [Header("Runtime refs")]
    [SerializeField] private RhythmEngine rhythmEngine;
    [SerializeField] private RhythmManager rhythmManager;

    private void Awake()
    {
        ApplyToSceneDependencies();
    }

    public void ApplyToSceneDependencies()
    {
        if (rhythmEngine != null)
            ApplyTo(rhythmEngine);

        if (rhythmManager != null)
            ApplyTo(rhythmManager);
    }

    public void ApplyTo(RhythmEngine engine)
    {
        if (engine == null)
            return;

        engine.mapManager = this;
        engine.bpm = bpm;
        engine.songClip = songClip;
        engine.levelDuration = levelDuration;
    }

    public void ApplyTo(RhythmManager manager)
    {
        if (manager == null)
            return;

        manager.mapManager = this;
        manager.X2_Sections = X2_Sections;
    }
}
