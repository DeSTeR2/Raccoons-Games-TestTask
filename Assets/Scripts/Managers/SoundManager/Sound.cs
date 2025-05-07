using System;
using UnityEngine;

public enum SoundType
{
    BoxMerged,
    Win
}

[Serializable]
public class Sound
{
    public SoundType type;
    public AudioClip audio;
    public float timeToPlayAgain;
}