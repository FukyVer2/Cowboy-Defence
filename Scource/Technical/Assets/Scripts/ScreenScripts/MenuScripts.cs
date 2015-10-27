using UnityEngine;
using System.Collections;

public class MenuScripts : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
    public void OnClick_Play()
    {
        ScreenController.Instance.isChangeScreen = true;
        ScreenController.Instance.statusScreen = StatusScreen.GamePlayScreen;
    }
	// Update is called once per frame
	void Update () {
	
	}
}
