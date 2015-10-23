using UnityEngine;
using System.Collections;

public class UIGun : MonoBehaviour {

    public BaseGunType gunType;

    public void ChangeGunHandelEvent()
    {
        GameController.Instance.player.ChangeGun(gunType);
    }
}
