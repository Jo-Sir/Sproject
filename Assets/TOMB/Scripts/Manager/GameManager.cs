using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public float mouseSensitivity;
    public float volume;
    private new void Awake()
    {
        base.Awake();
        mouseSensitivity = 800f;
        AudioListener.volume = 0.2f;
        SceneManager.sceneLoaded += LoadedsceneEvent;
    }
    private void LoadedsceneEvent(Scene scene, LoadSceneMode mode)
    {
        VolumeChange(volume);
    }
    public void VolumeChange(float volume)
    {
        AudioListener.volume = volume;
        this.volume = AudioListener.volume;
    }
    public void ReturnMain()
    {
        Time.timeScale = 1f;
        ObjectPoolManager.Instance.returnObjectAll?.Invoke();
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("MainMenu");
    }
}
