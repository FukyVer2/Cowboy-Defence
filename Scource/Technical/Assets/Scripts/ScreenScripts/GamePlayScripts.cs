using UnityEngine;
using System.Collections;

public class GamePlayScripts : MonoBehaviour {

	// Use this for initialization
	void Start () {
        AudioManager.Instance.Play(BaseAudioType.BA_GAMEPLAY_AUDIO, true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
