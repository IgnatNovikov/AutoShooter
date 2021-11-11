using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class PoolController
{
    protected List<PoolObject> _poolObjects;

    abstract public void AddObject(GameObject poolObject, Transform parentTransform);

    abstract public void AddObject(GameObject poolObject, Transform parentTransform, Transform spawnSpot);

    abstract public void AddMultipleObjects(PoolObject poolObject, Transform parentTransform, Transform spawnSpot, int count);

    public PoolObject GetObject(Transform parentTransform)
    {
        return GetObject(parentTransform, parentTransform);
    }

    public PoolObject GetObject(Transform parentTransform, Transform spawnSpot)
    {
        PoolObject free = GetFree();
        if (free != null)
        {
            return free;
        }

        AddObject(_poolObjects[0].gameObject, parentTransform, spawnSpot);
        return _poolObjects[_poolObjects.Count - 1];
    }


    private PoolObject GetFree()
    {
        for (int i = 0; i < _poolObjects.Count; i++)
        {
            if (_poolObjects[i].gameObject.activeInHierarchy == false)
            {
                return _poolObjects[i];
            }
        }
        return null;
    }

    public bool IsAllInactive()
    {
        foreach (PoolObject unit in _poolObjects)
        {
            if (unit.gameObject.activeSelf)
            {
                return false;
            }
        }

        return true;
    }
}