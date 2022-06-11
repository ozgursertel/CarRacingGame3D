using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public bool isVoiceOn;
    public Sound[] sounds;
    public static AudioManager instance;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        if(PlayerPrefs.GetInt("isVoiceOn",1)== 1)
        {
            isVoiceOn = true;
        }
        else
        {
            isVoiceOn = false;
        }
        setVoice();

    }

    private void Start()
    {
        Play("Main Theme");
    }

    private void Update()
    {

    }
    public void setVoice()
    {
        
        if (!isVoiceOn)
        {
            foreach(Sound s in sounds)
            {
  
                s.source.volume = 0;
            }
            
        }
        if (isVoiceOn)
        {
            foreach (Sound s in sounds)
            {
                
                s.source.volume = 1;
            }
        }
    }
    public void Play(string name)
    {

        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            return;
        }
        s.source.Play();
    }

    public void stopPlaying(String name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            return;
        }
        s.source.Stop();
    }
}
