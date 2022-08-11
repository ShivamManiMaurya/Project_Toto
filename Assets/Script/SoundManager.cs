using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Sound manager system form youtube Tarodev channel
    public static SoundManager Instance;

    [SerializeField] private AudioSource _musicClip, _effectClip;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void PlaySound(AudioClip clip, float volume)
    {
        _effectClip.PlayOneShot(clip, volume);
        
    }

    public void PlayMusic(AudioClip clip)
    {
        if (_musicClip.clip.name == clip.name)
        { return; }

        _musicClip.Stop();
        _musicClip.clip = clip;
        _musicClip.Play();
    }


}
