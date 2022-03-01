using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    private static AudioManager _instance = null;

    public Sound[] sounds;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

            s.source.playOnAwake = false;
            s.source.pitch = 1;
        }
    }

    private void Start()
    {
        Play("MainMusic");
        Sound s = Array.Find(sounds, sound => sound.name == "MainMusic");
        s.source.loop = true;
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log(name + " music is not found");
            return;
        }
        s.source.Play();
    }

    public void AdjustVolume(String name, float volume)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) return;
        s.source.volume = volume;
    }

    public void AdjustVolumeAllClip(float value)
    {
        foreach (Sound s in sounds)
        {
            s.source.volume = value;
        }
    }
}
