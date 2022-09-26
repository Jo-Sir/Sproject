using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    [SerializeField] Slider mouseSlider;
    [SerializeField] Slider soundSlider;
    private void Update()
    {
        SoundSlider();
        MouseSlider();
    }

    private void MouseSlider()
    {
        GameManager.Instance.mouseSensitivity = mouseSlider.value * 2000f;
    }

    private void SoundSlider()
    {
        GameManager.Instance.VolumeChange(soundSlider.value);
    }
}
