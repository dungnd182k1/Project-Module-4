using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] AudioSource audioBGM;
    [SerializeField] AudioSource audioSFX;
    [SerializeField] AudioClip clickButtonClip;

    public void ClickButton()
    {
        audioSFX.clip = clickButtonClip;
        audioSFX.Play();
    }
}