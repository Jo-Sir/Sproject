using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioClips;
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void BossTrace()
    {
        audioSource.clip = audioClips[0];
        AudioSourcePlay();
    }

    private void AudioSourcePlay()
    {
        audioSource.Play();
    }
}
