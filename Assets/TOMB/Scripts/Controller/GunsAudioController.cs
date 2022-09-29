using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GunsAudioController : AudioController
{
    private GunsAudioClip[] gunsAudioClip;
    private new void Awake()
    {
        base.Awake();
        gunsAudioClip = GetComponentsInChildren<GunsAudioClip>(true);
    }
    public void audioClipsChange(int curGunNum)
    {
        audioClips = gunsAudioClip[curGunNum].AudioClips; 
    }
    public void PlayReloadAudio()
    {
        audioSource.clip = audioClips[0];
        AudioSourcePlay();
    }
    public void PlayFireAudio()
    {
        audioSource.clip = audioClips[1];
        AudioSourcePlay();
    }

    public void PlaySwapAudio()
    {
        audioSource.clip = audioClips[2];
        AudioSourcePlay();
    }
    public void PlayReloadInsertAudio()
    {
        if (audioClips[3] == null) return;
        audioSource.clip = audioClips[3];
        AudioSourcePlay();
    }
}
