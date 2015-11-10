using UnityEngine;
using System.Collections;

public enum StatusScreen
{
    GameMenuScreen = 1,
    GameOverScreen = 2,
    GamePlayScreen = 3,
    Default = 4
}
public class ScreenController : MonoSingleton<ScreenController>
{
    public GameObject obj_MenuScreen;
    public GameObject obj_GameOverScreen;
    public GameObject obj_GamePlayScreen;

    public StatusScreen statusScreen;
    public bool isChangeScreen =false;
	// Use this for initialization
	void Start () {
        InitStartScreen();
        statusScreen = StatusScreen.Default;
	}
	
    void InitStartScreen()
    {
        if (obj_MenuScreen.activeInHierarchy == false)
        {
            obj_MenuScreen.SetActive(true);
        }
        if (obj_GameOverScreen.activeInHierarchy == true)
        {
            obj_GameOverScreen.SetActive(false);
        }
        if (obj_GamePlayScreen.activeInHierarchy == true)
        {
            obj_GamePlayScreen.SetActive(false);
        }
    }
    
    void UpdateScreen()
    {
        if(isChangeScreen)
        {
            isChangeScreen = false;
            switch(statusScreen)
            {
                case StatusScreen.GameMenuScreen:
                    obj_MenuScreen.SetActive(true);
                    obj_GameOverScreen.SetActive(false);
                    obj_GamePlayScreen.SetActive(false);
                    AudioManager.Instance.Play(BaseAudioType.BA_MENU_AUDIO, true);
                    break;
                case StatusScreen.GameOverScreen:
                    obj_MenuScreen.SetActive(false);
                    obj_GameOverScreen.SetActive(true);
                    obj_GamePlayScreen.SetActive(false);
                    break;
                case StatusScreen.GamePlayScreen:
                    obj_MenuScreen.SetActive(false);
                    obj_GameOverScreen.SetActive(false);
                    obj_GamePlayScreen.SetActive(true);
                    //AudioManager.Instance.Play(BaseAudioType.BA_GAMEPLAY_AUDIO, true);
                    break;
                default:
                    break;
            }
        }
    }
	// Update is called once per frame
	void Update () {
        UpdateScreen();
	}
}
