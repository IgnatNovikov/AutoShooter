using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolController
{
    private List<PoolObject> _poolObjects = new List<PoolObject>();

    protected GameObject _prefab;

    protected Transform _parentTransform;

    public PoolController(PoolObject prefab, int count)
    {
        _prefab = prefab.gameObject;

        for (int i = 0; i < count; i++)
        {
            AddObject(prefab);
        }
    }

    public void AddObject(PoolObject poolObject)
    {
        GameObject newObject = GameObject.Instantiate(_prefab.gameObject);
        newObject.SetActive(false);

        _poolObjects.Add(newObject.GetComponent<PoolObject>());
    }

    public GameObject GetObject()
    {
        GameObject freeObject;
        for (int i = 0; i < _poolObjects.Count; i++)
        {
            freeObject = _poolObjects[i].gameObject;

            if (!freeObject.activeInHierarchy)
                return freeObject;
        }

        AddObject(_poolObjects[0]);
        return _poolObjects[_poolObjects.Count - 1].gameObject;
    }

    public int GetCount()
    {
        return _poolObjects.Count;
    }
}