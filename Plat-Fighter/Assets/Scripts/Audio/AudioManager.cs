using UnityEngine.Audio;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
   
    public Sound[] sounds;

    void Awake()
    {
        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.playOnAwake = s.playOnAwake;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            Debug.Log("No such audio clip");
        s.source.Play();        
    }
    
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            Debug.Log("No such audio clip");
        s.source.Stop();        
    }

    public void PlaySoundRandomPitch(string name, float minPitch, float maxPitch)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            Debug.Log("No such audio clip");
        RandomizePitch(name, minPitch, maxPitch); 
        Play(name); 
    }

    private void RandomizePitch(string name, float minPitch, float maxPitch)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            Debug.Log("No such audio clip");
        s.source.pitch = UnityEngine.Random.Range(minPitch, maxPitch);
    }

}
