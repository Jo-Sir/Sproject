using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameOptionController : MonoBehaviour
{
    [SerializeField] private GameObject backGround;
    [SerializeField] private GameObject _AllOptions;
    private bool isShowUI = false;

    private void Update()
    {
        if (Input.GetButtonDown("Cancel") && !isShowUI) { ShowUI(); }
        else if (Input.GetButtonDown("Cancel") && isShowUI) { HideUI(); }
    }
    private void ShowUI()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        isShowUI = true;
        backGround.SetActive(true);
        _AllOptions.SetActive(true);
    }
    private void HideUI()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        isShowUI = false;
        backGround.SetActive(false);
        _AllOptions.SetActive(false);
    }

    public void ScreenModeChange()
    {
        if (Screen.fullScreenMode != FullScreenMode.ExclusiveFullScreen)
        { Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen; }
        else { Screen.fullScreenMode = FullScreenMode.Windowed; }
        /*Screen.SetResolution(1920, 1080, true);
        Screen.SetResolution(1280, 720, false);*/
    }
    public void OnClickMainMenu()
    {
        PlayerManager.Instance.playerUI.IsDie();
        StartCoroutine(ReturnMainMenu());
    }
    public void OnClickQuit()
    {
        Application.Quit();
    }
    private IEnumerator ReturnMainMenu() 
    {
        yield return new WaitForSecondsRealtime(2f);
        GameManager.Instance.ReturnMain();
    }
}
