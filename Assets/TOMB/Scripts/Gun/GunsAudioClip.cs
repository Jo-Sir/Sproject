using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunsAudioClip : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioClips;
    public AudioClip[] AudioClips { get { return audioClips; } }
}
