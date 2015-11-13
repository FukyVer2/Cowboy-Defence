using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIGunController : MonoSingleton<UIGunController> {

    public List<UIGun> gunUI;
    private Dictionary<BaseGunType, UIGun> dicGunUI;

    void Awake()
    {
        dicGunUI = new Dictionary<BaseGunType, UIGun>();
        InitGunUI();
    }

    void InitGunUI()
    {
        foreach (var item in gunUI)
        {
            dicGunUI.Add(item.gunType, item);
        }
    }

    public UIGun GetGunUI(BaseGunType _gunType)
    {
        if(dicGunUI.ContainsKey(_gunType))
        {
            return dicGunUI[_gunType];
        }
        return null;
    }
}
