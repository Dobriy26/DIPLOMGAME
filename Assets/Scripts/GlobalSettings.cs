using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSettings
{
    private static float volume=1f;
    private static bool mute = false;
    public static void SetVolume(float vol)
    {
        volume = vol;
    }

    public static float GetVolume()
    {
        return volume;
    }

    public static void SetMute(bool mut)
    {
        mute = mut;
    }
    public static bool GetMute()
    {
        return mute;
    }
    
    
}
