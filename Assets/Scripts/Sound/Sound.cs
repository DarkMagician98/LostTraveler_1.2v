using UnityEngine.Audio;
using UnityEngine;


[System.Serializable]
public class Sound
{
    // Start is called before the first frame update

    public string name;

    public AudioClip clip;

    [Range(0, 1)]
    public float volume;

    [Range(.1f, 3.0f)]
    public float pitch;

    public bool loop;

    public float panStereo;

    public AudioMixerGroup mixerGroup;

    [HideInInspector]
    public AudioSource source;

    public bool isPlaying;
}
