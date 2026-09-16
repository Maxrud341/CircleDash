using UnityEngine;
using UnityEngine.UI;

public abstract class BarSettings : MonoBehaviour
{
    public ApplySettings applySettings;
    public float settingsLvl;
    protected Scrollbar scrollbar;



    protected virtual void OnEnable()
    {
        Debug.Log("[BarSettings] OnEnable called");
        scrollbar = GetComponent<Scrollbar>();
        settingsLvl = GetLvl();
        ChangeScrollbarValue();

        scrollbar.onValueChanged.AddListener(OnScrollbarChanged);
    }

    protected abstract float GetLvl();
    protected virtual void OnDisable()
    {
        scrollbar.onValueChanged.RemoveListener(OnScrollbarChanged);
    }

    protected virtual void OnScrollbarChanged(float value)
    {
        Debug.Log($"[BarSettings] OnScrollbarChanged called with value: {value}");
        settingsLvl = value;
        ChangeSettings();
    }

    public virtual void ChangeSettings()
    {
        Debug.Log($"[BarSettings] ChangeSettings called with settingsLvl: {settingsLvl}");
    }

    public virtual void ChangeScrollbarValue()
    {
        Debug.Log($"[BarSettings] ChangeScrollbarValue called, setting value to: {settingsLvl}");
        scrollbar.value = settingsLvl;
    }
}