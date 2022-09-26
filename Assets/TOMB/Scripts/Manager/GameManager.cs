using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [Range(0f, 2000f)] public float mouseSensitivity;

    public void OnClickStart()
    {
        SceneManager.LoadScene("NomalStage");
    }
    public void OnClickQuit()
    {
        Debug.Log("¡æ∑·«‘");
        Application.Quit();
    }
}
