using UnityEngine;
using System;

public class AudioManager : MonoBehaviour {

    public Sound[] sounds;
    public static AudioManager instance = null;

    private Sound currentSong;

    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.playOnAwake = s.playOnAwake;
            //Debug.Log("clip : " + s.name);
        }
    }

    void Start()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        Play("Blood and Steel Loop");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        currentSong = s;
        s.source.Play();
    }

    public void Stop()
    {
        if (currentSong == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        currentSong.source.Stop();
    }

    public void SetVol(float newVolume)
    {
        instance.currentSong.source.volume = newVolume;
    }
    
}
