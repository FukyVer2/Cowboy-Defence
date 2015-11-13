using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GunController : MonoSingleton<GunController>
{
    public List<GunConfig> listGunConfig;
    public GameObject gunCurrent;
    public Vector3 targetPosition;
    public bool isAutoShoot; // sung nay nhap tay giu nguyen thi ban
    private ControlGun gun;
    public Dictionary<BaseGunType, GameObject> dicGunResources; // loại đạn theo súng
    void Awake()
    {
        dicGunResources = new Dictionary<BaseGunType, GameObject>();
        //InitGun();
    }

    public virtual void InitGun()
    {
        foreach (GunConfig item in listGunConfig)
        {
            dicGunResources.Add(item.gunType, item.gunObject);
        }
    }
    
    // get gun config from baseGunType
    public GunConfig GetGunConfig(BaseGunType _gunType)
    {
        foreach(GunConfig config in listGunConfig)
        {
            if(config.gunType == _gunType)
            {
                return config;
                break;
            }
        }
#if UNITY_EDITOR
        Debug.Log("khong get duoc gun config !");
#endif
        return null;
    }
    public void SetGun(BaseGunType _gunType)
    {
        GameObject gunObject = GetGunOfGunType(_gunType);
        if (gunObject != null)
        {
            if (gunCurrent != null)
            {
                gunCurrent.GetComponent<SpriteRenderer>().enabled = false;
            }
            gunCurrent = gunObject;
            gunCurrent.GetComponent<SpriteRenderer>().enabled = true;
            gun = this.gunCurrent.GetComponent<ControlGun>();
        }
    }

    void Update()
    {
        if (gun != null && gun.auto && isAutoShoot)
        {
            this.gun.GunShoot(targetPosition);
        }
    }

    public void GunStop()
    {
        isAutoShoot = false;
    }

    public void ShootSpawn(Vector3 _mousePosition)
    {
        if (gun != null)
        {
            if (!gun.auto)
            {
                this.gun.GunShoot(_mousePosition);
            }
            else
            {
                isAutoShoot = true;
                targetPosition = _mousePosition;
            }
        }
    }

    public bool IsAutoGun()
    {
        if (gun != null)
        {
            return gun.auto;
        }
        return false;
    }

    private GameObject GetGunOfGunType(BaseGunType _gunType)
    {
        if (dicGunResources.ContainsKey(_gunType))
            return dicGunResources[_gunType];
#if UNITY_EDITOR
        Debug.Log("Chưa có viên đạn loại này");
#endif
        return null;
    }
}

[System.Serializable]
public class GunConfig
{
    public BaseGunType gunType;
    public GameObject gunObject;
}
