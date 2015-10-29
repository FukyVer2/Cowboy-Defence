using UnityEngine;
using System.Collections;

public class GameOverScreen : MonoBehaviour {
    public string nameLevelReload;
    public void OnClick_Reset()
    {
        Application.LoadLevel(nameLevelReload);
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
