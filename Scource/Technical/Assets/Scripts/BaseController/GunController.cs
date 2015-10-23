using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GunController : MonoBehaviour
{

    public List<GunConfig> listGunConfig;
    public GameObject gunCurrent;
    public Vector3 targetPosition;
    public bool isAutoGun; // sung nay nhap tay giu nguyen thi ban
    private ControlGun gun;
    private Dictionary<BaseGunType, GameObject> dicGunResources; // loại đạn theo súng

    void Awake()
    {
        dicGunResources = new Dictionary<BaseGunType, GameObject>();
        InitGun();
    }

    public virtual void InitGun()
    {
        foreach (GunConfig item in listGunConfig)
        {
            dicGunResources.Add(item.gunType, item.gunObject);
        }
    }

    public void SetGun(BaseGunType _gunType)
    {
        GameObject gunObject = GetGunOfGunType(_gunType);
        if (gunObject != null)
        {
            if (gunCurrent != null)
            {
                gunCurrent.SetActive(false);
            }
            gunCurrent = gunObject;
            gunCurrent.SetActive(true);
            gun = this.gunCurrent.GetComponent<ControlGun>();
        }
    }

    void Update()
    {
        if (gun != null && gun.auto && isAutoGun)
        {
            this.gun.GunShoot(targetPosition);
        }
    }

    public void GunStop()
    {
        isAutoGun = false;
    }

    public void ShootSpawn(Vector3 _mousePosition)
    {
        if (gun != null)
        {
            if (!gun.auto)
            {
                Debug.Log("ban-ban-ban");
                this.gun.GunShoot(_mousePosition);
            }
            else
            {
                isAutoGun = true;
                targetPosition = _mousePosition;
            }
        }
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
