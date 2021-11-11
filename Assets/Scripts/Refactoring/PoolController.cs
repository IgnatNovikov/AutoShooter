using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class PoolController
{
    private List<PoolObject> _poolObjects = new List<PoolObject>();
    private List<PoolObject> _activePoolObjects = new List<PoolObject>();

    abstract public void Initialize();

    abstract public void AddObject(PoolObject poolObject, Transform parentTransform, Transform spawnSpot);

    abstract public void AddMultipleObjects(PoolObject poolObject, Transform parentTransform, Transform spawnSpot, int count);

    abstract public PoolObject GetObject();

    public bool AllInactive()
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