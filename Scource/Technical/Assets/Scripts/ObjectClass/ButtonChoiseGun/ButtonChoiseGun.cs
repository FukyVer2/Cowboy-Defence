using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonChoiseGun : MonoBehaviour {
    public BaseGunType gunType;
    public GameObject objParent;
	// Use this for initialization
	void Start () {
	}
	
    public void OnClick()
    {
        GunConfig gunConfig = GunController.Instance.GetGunConfig(this.gunType);
        //Debug.Log(gunConfig.gunType);
        GunController.Instance.dicGunResources.Add(gunConfig.gunType, gunConfig.gunObject);
        ChoiseGunController.Instance.countbuttonChoiseCurrent++;
        GunController.Instance.SetGun(gunType);
        //objParent.SetActive(false);
        this.enabled = false;
    }
	// Update is called once per frame
	void Update () {
	
	}
}
