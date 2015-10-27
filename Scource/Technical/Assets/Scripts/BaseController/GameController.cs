using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoSingleton<GameController> {

    public ControlGun playerGun;
    public PlayerController player;
    public List<Sprite> listSpriteNumber;
	// Update is called once per frame
	void Start () {
        AudioManager.Instance.Play(BaseAudioType.BA_WORKD_AUDIO, true);
	}
}
