using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Range(0, 1.0f)]
    public float volumeSlider;
    float previousVolume;
    public Sound[] sounds;

    [Range(0, 1.0f)]
    public float sfxvolumeSlider;
    float sfxpreviousVolume;
    public Sound[] sfx;

    [Range(0, 1.0f)]
    public float musicvolumeSlider;
    float musicpreviousVolume;
    public Sound[] music;

    public static AudioManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        #region Copy this for both sfx and music
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        previousVolume = volumeSlider;
        #endregion

    }

    private void Update()
    {
        if(volumeSlider != previousVolume)
        {
            foreach(Sound s in sounds)
            {
                s.source.volume = s.volume * previousVolume;
            }
        }

        previousVolume = volumeSlider;
    }

    public void SliderValue(Slider slider)
    {
        volumeSlider = slider.value;
    }

    void Start()
    {
        
        Play("CaveSounds");
    }

    // Update is called once per frame
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }
    public void PlayP(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
        s.source.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
        
    }
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }
    public void PlayVolume(string name, float volume)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.volume = volume;
    }
    public void StopVolume(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.volume = 0;
    }

}
