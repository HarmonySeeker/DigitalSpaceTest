using UnityEngine;

public static class AudioNames
{
    public enum Sound
    {
        PlayerWalk,
        PlayerSprint,
        PlayerJump,
        StartRace,
        FinishRace
    }
}

[System.Serializable]
public struct SoundEffect
{
    public AudioNames.Sound sound;
    public AudioClip[] clips;
}

public class AudioLibrary : MonoBehaviour
{
    public SoundEffect[] soundEffects;
    public AudioClip GetClipFromName(AudioNames.Sound soundName)
    {
        foreach (var soundEffect in soundEffects)
        {
            if (soundEffect.sound == soundName)
            {
                return soundEffect.clips[Random.Range(0, soundEffect.clips.Length)];
            }
        }
        return null;
    }
}
