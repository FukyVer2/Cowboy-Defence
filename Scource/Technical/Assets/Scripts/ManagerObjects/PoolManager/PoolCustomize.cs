using UnityEngine;
using System.Collections;
using PathologicalGames;

public class PoolCustomize : MonoSingleton<PoolCustomize> {

    public GameObject GetBaseObject(GameObject _prefab, string poolName)
    {

        if (PoolManager.Pools.ContainsKey(poolName))
        {
            SpawnPool spawPool = PoolManager.Pools[poolName];
            if (!spawPool.IsSpawned(_prefab.transform))
            {
                return spawPool.Spawn(_prefab.transform).gameObject;
            }
        }
#if UNITY_EDITOR
            Debug.LogWarning("Khong co pool nay bo oi! Lam on tao giup con cai! Hehe");
#endif
        return null;
    }

    public GameObject GetBaseObject(GameObject _prefab, Vector3 _position, string _poolName)
    {

        if (PoolManager.Pools.ContainsKey(_poolName))
        {
            SpawnPool spawPool = PoolManager.Pools[_poolName];
            if (!spawPool.IsSpawned(_prefab.transform))
            {
                return spawPool.Spawn(_prefab.transform, _position, Quaternion.identity).gameObject;
            }
        }
#if UNITY_EDITOR
        Debug.LogWarning("Khong co pool nay bo oi! Lam on tao giup con cai! Hehe");
#endif
        return null;
    }

    public void HideBaseObject(GameObject _prefab, string poolName)
    {
        if (PoolManager.Pools.ContainsKey(poolName))
        {
            SpawnPool spawPool = PoolManager.Pools[poolName];
            if (spawPool.IsSpawned(_prefab.transform))
            {
                spawPool.Despawn(_prefab.transform);
            }
        }
        else
        {
#if UNITY_EDITOR
            Debug.LogWarning("Khong co pool nay bo oi! Lay dau ra ma cat cho bo ha! Hehe");
#endif
        }
    }

    public void HideBaseObject(GameObject _prefab, string poolName,float time)
    {
        if (PoolManager.Pools.ContainsKey(poolName))
        {
            SpawnPool spawPool = PoolManager.Pools[poolName];
            if (spawPool.IsSpawned(_prefab.transform))
            {
                spawPool.Despawn(_prefab.transform,time);
            }
        }
        else
        {
#if UNITY_EDITOR
            Debug.LogWarning("Khong co pool nay bo oi! Lay dau ra ma cat cho bo ha! Hehe");
#endif
        }
    }
    
}
