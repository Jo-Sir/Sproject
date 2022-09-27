using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public float mouseSensitivity;
    private new void Awake()
    {
        base.Awake();
        mouseSensitivity = 800f;
        AudioListener.volume = 0.2f;
    }
    public void VolumeChange(float volume)
    {
        AudioListener.volume = volume;
    }
    public void ReturnMain()
    {
        Time.timeScale = 1f;
        ObjectPoolManager.Instance.returnObjectAll?.Invoke();
        SceneManager.LoadScene("MainMenu");
    }
}
