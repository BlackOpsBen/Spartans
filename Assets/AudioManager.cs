using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public const int SFX_SHIELD_HIT = 0;
    public const int SFX_SHUFFLE = 1;
    public const int SFX_MARCH = 2;

    public const int DIALOG_HAOO = 0;
    public const int DIALOG_SPARTAN_DEATH = 1;
    public const int DIALOG_ENEMY_DEATH = 2;
    public const int DIALOG_KILL = 3;

    public static AudioManager Instance { get; private set; }

    [Header("Specific SFX")]
    public Sound[] SFX;

    [Header("SFX Categories")]
    public SoundGroup[] soundGroups;

    [Header("Music")]
    public SoundCategory musicCategory;

    private bool soundIsPlaying = false;
    private float soundDuration = 0f;

    private void Awake()
    {
        SingletonPattern();

        CreateAudioSources(ref SFX);

        foreach (SoundGroup sg in soundGroups)
        {
            foreach (SoundCategory dc in sg.soundCategories)
            {
                CreateAudioSources(ref dc.soundOptions);
            }
        }

        CreateAudioSources(ref musicCategory.soundOptions);
    }

    private void Start()
    {
        StartMusic();
    }

    private void SingletonPattern()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        if (soundDuration < 0.0f)
        {
            soundIsPlaying = false;
        }
        else
        {
            soundDuration -= Time.deltaTime;
        }
    }

    private void CreateAudioSources(ref Sound[] sounds)
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(SFX, sound => sound.name == name);
        s.source.Play();
    }

    public void PlaySFXLoop(string name)
    {
        Sound s = Array.Find(SFX, sound => sound.name == name);
        s.source.loop = true;
        if (!s.source.isPlaying)
        {
            s.source.Play();
        }
    }

    public void StopSFXLoop(string name)
    {
        Sound s = Array.Find(SFX, sound => sound.name == name);
        s.source.loop = false;
        s.source.Stop();
    }

    private void StartMusic()
    {
        int rand = UnityEngine.Random.Range(0, musicCategory.soundOptions.Length);

        Sound s = musicCategory.soundOptions[rand];
        s.source.loop = true;
        s.source.Play();
    }

    public void PlaySoundFromGroup(int groupIndex, int SOUND_CATEGORY, bool oneAtATime)
    {
        int maxOption = soundGroups[groupIndex].soundCategories[SOUND_CATEGORY].soundOptions.Length;
        int selectedOption = UnityEngine.Random.Range(0, maxOption);
        Sound s = soundGroups[groupIndex].soundCategories[SOUND_CATEGORY].soundOptions[selectedOption];

        if (oneAtATime)
        {
            if (soundIsPlaying)
            {
                return;
            }
            soundDuration = s.source.clip.length;
            soundIsPlaying = true;
        }

        s.source.Play();
    }

    public bool GetIsSoundPlaying()
    {
        return soundIsPlaying;
    }
}

[System.Serializable]
public class SoundGroup
{
    public string name;
    public SoundCategory[] soundCategories;
}

[System.Serializable]
public class SoundCategory
{
    public string name;
    public Sound[] soundOptions;
}
