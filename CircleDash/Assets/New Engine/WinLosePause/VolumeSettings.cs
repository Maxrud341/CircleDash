using UnityEngine;

public class VolumeSettings : BarSettings
{
    public VolumeTypes volumeType;

    protected override float GetLvl()
    {
        if (volumeType == VolumeTypes.Music)
        {
            return applySettings.musicVolume;
        }
        else if (volumeType == VolumeTypes.SFX)
        {
            return applySettings.sfxVolume;
        }
        return 1f;
    }

    public override void ChangeSettings()
    {
        applySettings.ApplyVolume(settingsLvl, volumeType);
        Debug.Log($"[VolumeSettings] ChangeSettings called, setting volume to: {settingsLvl}");
    }
}

public enum VolumeTypes
{
    Music,
    SFX
}