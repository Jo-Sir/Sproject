using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    private Animator anim;

    [SerializeField] private string newGameSceneName;
    public int quickSaveSlotID;

    [Header("Options Panel")]
    public GameObject MainOptionsPanel;
    public GameObject StartGameOptionsPanel;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();

        //new key
        //PlayerPrefs.SetInt("quickSaveSlot", quickSaveSlotID);
    }

    private void Awake()
    {
        if (PlayerPrefs.GetInt("Mute", 0) == 1)
        {
            AudioListener.volume = 0;
        }
        else
        {
            AudioListener.volume = 1;
        }
    }

    #region Open Different panels

    public void OnClickOptions()
    {
        //enable respective panel
        MainOptionsPanel.SetActive(true);

        //play anim for opening main options panel
        //anim.Play("buttonTweenAnims_on");
        anim.Play("OptTweenAnim_on");
        //play click sfx
        PlayClickSound();

        //enable BLUR
        //Camera.main.GetComponent<Animator>().Play("BlurOn");

    }

    public void OnClickStartGame()
    {
        PlayClickSound();
        GameStart();
    }
    public void GameStart()
    {
        if (!string.IsNullOrEmpty(newGameSceneName))
            SceneManager.LoadScene(newGameSceneName);
        else
            Debug.Log("Please write a scene name in the 'newGameSceneName' field of the Main Menu Script and don't forget to " +
                "add that scene in the Build Settings!");
    }
    #endregion

    #region Back Buttons

    public void Back_options()
    {
        //simply play anim for CLOSING main options panel
        //anim.Play("OptTweenAnim_off");
        anim.Play("buttonTweenAnims_off");
        //disable BLUR
        // Camera.main.GetComponent<Animator>().Play("BlurOff");

        //play click sfx
        PlayClickSound();
    }

    public void Quit()
    {
        Application.Quit();
    }
    #endregion

    #region Sounds
    public void PlayHoverClip()
    {

    }

    void PlayClickSound()
    {

    }


    #endregion
}
