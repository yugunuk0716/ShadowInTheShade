using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{

    public AudioSource _sfxSource;
    public AudioSource _bgmSource;


    [Header("SFX")]
    public AudioClip _doorOpenSFX;
    public AudioClip _slimeMoveSFX;
    public AudioClip _slimeHitSFX;
    public AudioClip _playerDashSFX;
    public AudioClip _playerAttackSFX;
    public AudioClip _playerHitSFX;

    [Header("BGM")]
    public AudioClip _stage1BGM;

   


    private void Start()
    {
        //DontDestroyOnLoad(this.gameObject);
        PlayBGM(_stage1BGM);
        //GameManager.Instance.OnPlayerAttack.AddListener(() =>
        //{
        //    if (GameManager.Instance.isAttack)
        //    {
        //        PlaySFX(_playerAttackSFX);
        //    }
        //});
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
