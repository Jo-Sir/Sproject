using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] protected AudioClip[] audioClips;
    protected AudioSource audioSource;
    protected void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void BossTrace()
    {
        audioSource.clip = audioClips[0];
        AudioSourcePlay();
    }
    public void PlayerDie()
    {
        audioSource.clip = audioClips[1];
        AudioSourcePlay();
    }
    public void Clear()
    {
        audioSource.clip = audioClips[2];
        AudioSourcePlay();
    }
    protected void AudioSourcePlay()
    {
        audioSource.Play();
    }
}
