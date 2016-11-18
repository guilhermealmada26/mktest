using System;
using UnityEngine;

public class GameAuxiliary
{
    public static void PlaySound(AudioSource source, AudioClip clip)
    {
        source.clip = clip;
        source.Play();
    }

    public static void StopSounds(AudioSource source)
    {
        source.Stop();
    }
}
