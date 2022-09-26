using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public float mouseSensitivity;
    public void VolumeChange(float volume)
    {
        AudioListener.volume = volume;
    }
    public void ReturnMain()
    {
        ObjectPoolManager.Instance.returnObjectAll?.Invoke();
        SceneManager.LoadScene("MainMenu");
    }
}
