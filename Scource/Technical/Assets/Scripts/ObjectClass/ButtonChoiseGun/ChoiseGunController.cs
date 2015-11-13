using UnityEngine;
using System.Collections;

public class ChoiseGunController : MonoSingleton<ChoiseGunController> {
    public int countButtonChoise = 2;
    public int countbuttonChoiseCurrent = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(countbuttonChoiseCurrent == countButtonChoise)
        {
            gameObject.SetActive(false);
        }
	}
}
