﻿using UnityEngine;
using System.Collections;

public class GameController : MonoSingleton<GameController> {

    public ControlGun playerGun;
	// Update is called once per frame
	void Start () {
        AudioManager.Instance.Play(BaseAudioType.BA_WORKD_AUDIO, true);
	}
}
