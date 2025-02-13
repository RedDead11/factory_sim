using System;
using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;
    public static AudioManager instance;


    private void Awake()
    {

        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;


            //if (s.isMusic)
            //{
            //	s.source.mute = !PersistentDataManager.instance.GData.isMusic;
            //}
            //else s.source.mute = !PersistentDataManager.instance.GData.isSound;
        }
    }
    Sound s;

    public void PlaySound(string name)
    {
        s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            print(name + "Sound Not Found :(");
            return;
        }
        s.source.Play();
    }

    Sound prevMusic;
    public void PlayMusic(string name)
    {
        if (prevMusic != null && prevMusic.name == name)
            return;

        if (prevMusic != null && prevMusic.name != name)
        {
            if (prevMusic.source.isPlaying)
            {
                StartCoroutine(FadeOut(prevMusic, 1f)); // Fade out over 1 second
            }
        }

        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log(name + " Sound Not Found :(");
            return;
        }

        prevMusic = s;
        StartCoroutine(FadeIn(s, 1f)); // Fade in over 1 second
    }

    private IEnumerator FadeOut(Sound sound, float fadeTime)
    {
        float startVolume = sound.source.volume;

        while (sound.source.volume > 0)
        {
            sound.source.volume -= startVolume * Time.deltaTime / fadeTime;
            yield return null;
        }

        sound.source.Stop();
        sound.source.volume = startVolume;
    }

    private IEnumerator FadeIn(Sound sound, float fadeTime)
    {
        sound.source.Play();
        sound.source.volume = 0f;

        while (sound.source.volume < 1f)
        {
            sound.source.volume += Time.deltaTime / fadeTime;
            yield return null;
        }

        sound.source.volume = 1f;
    }


    public void Stop(string name)
    {
        s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            print(name + "Sound Not Found :(");
            return;
        }
        s.source.Stop();
    }

    public void SetSoundVolume(float val)
    {
        //effectVolume = val;
        foreach (Sound s in sounds)
        {
            if (!s.isMusic)
            {
                s.source.volume = val;
            }
        }
    }

    public void SetMusicVolume(float val)
    {
        //musicVolume = val;
        foreach (Sound s in sounds)
        {
            if (s.isMusic)
            {
                s.source.volume = val;
            }
        }
    }

    public void MuteEverything(bool val)
    {
        // Find all AudioSource components in the scene
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();

        // Loop through each AudioSource and toggle the mute property
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.mute = val;
        }

        // Log the mute state of the first AudioSource to verify the action
        if (audioSources.Length > 0)
        {
            Debug.Log("AudioSource mute state: " + audioSources[0].mute);
        }
        else
        {
            Debug.Log("No AudioSources found in the scene.");
        }

    }

    public void MuteSoundVolume(bool val)
    {
        foreach (Sound s in sounds)
        {
            if (!s.isMusic)
            {
                s.source.mute = val;
            }
        }
    }
    public void MuteMusicVolume(bool val)
    {
        foreach (Sound s in sounds)
        {
            if (s.isMusic)
            {
                s.source.mute = val;
            }
        }
    }


}
