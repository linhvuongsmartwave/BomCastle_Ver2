using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public AudioClip win;
    public AudioClip coin;
    public AudioClip bomExp;
    public AudioClip coolDown;
    public AudioClip openPopup;
    public AudioClip buttonClick;
    public AudioSource audioSoure;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        audioSoure = GetComponent<AudioSource>();
    }

    public void BomExp()
    {
        audioSoure.PlayOneShot(bomExp);
    }

    public void CoolDown()
    {
        audioSoure.PlayOneShot(coolDown);
    }

    public void AudioOpen()
    {
        audioSoure.PlayOneShot(openPopup);
    }

    public void AudioButtonClick()
    {
        audioSoure.PlayOneShot(buttonClick);
    }

    public void AudioCoin()
    {
        audioSoure.PlayOneShot(coin);
    }

    public void AudioWin()
    {
        audioSoure.PlayOneShot(win);
    }

    public void SetActive(bool isActive)
    {
        if (isActive) audioSoure.volume = 1f;
        else audioSoure.volume = 0f;
    }
}
