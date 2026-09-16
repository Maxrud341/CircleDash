using UnityEngine;
using UnityEngine.UI;

public class LatencySettings : BarSettings
{

    protected override void OnScrollbarChanged(float value)
    {
        Debug.Log($"[LatencySettings] OnScrollbarChanged called with value: {value}");
        settingsLvl = value - 0.5f;
        ChangeSettings();
    }

    protected override float GetLvl()
    {
        return applySettings.latency;
    }
    public override void ChangeSettings()
    {
        Debug.Log($"[LatencySettings] ChangeSettings called, setting latency to: {settingsLvl}");
        applySettings.ApplyLatency(settingsLvl);
    }

    public override void ChangeScrollbarValue()
    {
        Debug.Log($"[LatencySettings] ChangeScrollbarValue called, setting scrollbar value to: {settingsLvl + 0.5f}");
        scrollbar.value = settingsLvl + 0.5f;
    }
}
