using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class PoolController
{
    protected GameObject _prefab;

    protected Transform _parentTransform;
    protected Transform _spawnSpot;

    abstract public void AddObject(GameObject poolObject);

    abstract public void AddMultipleObjects(PoolObject poolObject, int count);

    public PoolObject GetObject(Transform parentTransform)
    {
        return GetObject(parentTransform, parentTransform);
    }

    abstract protected PoolObject GetObject(Transform parentTransform, Transform spawnSpot);


    abstract protected PoolObject GetFree();
}