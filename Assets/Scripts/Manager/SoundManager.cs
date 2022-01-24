using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{

    public AudioSource _sfxSource;
    public AudioSource _bgmSource;


    [Header("SFX")]
    public AudioClip _doorOpenSFX;

    [Header("BGM")]
    public AudioClip _stage1BGM;

   


    private void Start()
    {
        PlayBGM(_stage1BGM);
    }

    public void PlayBGM(AudioClip clip, float volume = 0.3f)
    {
        _bgmSource.clip = clip;
        _bgmSource.volume = volume;
        _bgmSource.Play();
    }

    public void PlaySFX(AudioClip clip, float volume = 0.3f)
    {
        _sfxSource.PlayOneShot(clip, volume);
    }


}
