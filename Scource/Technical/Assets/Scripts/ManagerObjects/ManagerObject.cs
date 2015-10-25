using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ManagerObject : MonoSingleton<ManagerObject> {

    public List<ObjectConfig> objectResources;
    private Dictionary<BaseObjectType, GameObject> dicObjectResources;

    void Awake()
    {
        dicObjectResources = new Dictionary<BaseObjectType, GameObject>();
        InitDictionary();
    }

    void InitDictionary()
    {
        foreach (var item in objectResources)
        {
            dicObjectResources.Add(item.objectType, item.objectPrefab);
        }
    }

    public GameObject GetPrefabsByObjectType(BaseObjectType _objectType)
    {
        if(dicObjectResources.ContainsKey(_objectType)){
            return dicObjectResources[_objectType];
        }
        return null;

    }

    public GameObject SpawnEnemy(BaseObjectType _objectType, Vector3 _position){
        GameObject prefab = GetPrefabsByObjectType(_objectType);
        if(prefab != null){
           return PoolCustomize.Instance.GetBaseObject(prefab, _position, "Enemy");
        }
        else
        {
#if UNITY_EDITOR
            Debug.LogError("Thang cha nay no chua co!");
#endif
            return null;
        }
    }

    public GameObject SpawnPartical(BaseObjectType _objectType, Vector3 _position)
    {
        GameObject prefab = GetPrefabsByObjectType(_objectType);
        if (prefab != null)
        {
            return PoolCustomize.Instance.GetBaseObject(prefab, _position, "Partical");
        }
        else
        {
#if UNITY_EDITOR
            Debug.LogError("Thang cha nay no chua co!");
#endif
            return null;
        }
    }
}

[System.Serializable]
public class ObjectConfig{
    public BaseObjectType objectType;
    public GameObject objectPrefab;
}