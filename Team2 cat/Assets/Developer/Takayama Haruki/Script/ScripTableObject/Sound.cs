using UnityEngine;

public enum SoundType
{
    SE,
    BGM,
}

[CreateAssetMenu(fileName ="SoundData", menuName = "Scriptable Objects/SoundData")]

public class SoundData : ScriptableObject
{
    public SoundType soundtype;
    public AudioClip[] sound;
}