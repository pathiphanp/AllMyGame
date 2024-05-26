using System;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public Sound[] musicSound, sfxSound;
    public AudioSource musicSource, sfxSource;

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSound, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound not found");
        }

        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }
    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSound, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }
    public AudioClip SearchSFX(String name)
    {
        Sound s = Array.Find(sfxSound, x => x.name == name);
        AudioClip _sfx = null;
        if (s == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            _sfx = s.clip;
        }
        return _sfx;
    }

    public void PlaySFXInObject(AudioSource sfx, String name)
    {
        sfx.clip = SearchSFX(name);
        sfx.Play();

        //Setting volume
        sfx.volume = sfxSource.volume;
    }
    public void StopSFXInObject(AudioSource sfx)
    {
        sfx.Stop();
    }
}
