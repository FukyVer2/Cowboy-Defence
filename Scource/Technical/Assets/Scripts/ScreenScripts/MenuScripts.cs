using UnityEngine;
using System.Collections;

public class MenuScripts : MonoBehaviour {

	// Use this for initialization
	void Start () {
        AudioManager.Instance.Play(BaseAudioType.BA_MENU_AUDIO, true);
	}
	
    public void OnClick_Play()
    {
        ScreenController.Instance.isChangeScreen = true;
        ScreenController.Instance.statusScreen = StatusScreen.GamePlayScreen;
        AudioManager.Instance.Stop(BaseAudioType.BA_MENU_AUDIO);
    }
	// Update is called once per frame
	void Update () {
	
	}
}
