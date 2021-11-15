using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PoolController
{
    private List<PoolObject> _poolObjects = new List<PoolObject>();
    private List<int> _freeObjects = new List<int>();

    protected GameObject _prefab;
    protected Transform _parentTransform;

    public PoolController(PoolObject prefab, int count)
    {
        _prefab = prefab.gameObject;

        for (int i = 0; i < count; i++)
        {
            AddObject();
        }
    }

    public GameObject GetObject()
    {
        if (_freeObjects.Count == 0)
        {
            AddObject();
            return _poolObjects[_poolObjects.Count - 1].gameObject;
        }
        
        int free = _freeObjects[0];
        _freeObjects.RemoveAt(0);

        _poolObjects[free].onReturn = Return;

        return _poolObjects[free].gameObject;
    }

    public List<GameObject> GetAllActive()
    {
        List<GameObject> allActive = new List<GameObject>();
        for (int i = 0; i < _poolObjects.Count; i++)
        {
            if (!_freeObjects.Contains(i))
            {
                allActive.Add(_poolObjects[i].gameObject);
            }
        }

        return allActive;
    }

    public void Refresh()
    {
        _freeObjects.Clear();

        for (int i = 0; i < _poolObjects.Count; i++)
        {
            _poolObjects[i].gameObject.SetActive(false);
            _freeObjects.Add(i);
        }
    }

    private void AddObject()
    {
        GameObject newObject = GameObject.Instantiate(_prefab.gameObject);
        newObject.SetActive(false);
        _freeObjects.Add(_poolObjects.Count);

        _poolObjects.Add(newObject.GetComponent<PoolObject>());
    }

    private void Return(GameObject returnedObject)
    {
        if (returnedObject == null)
            return;
        _freeObjects.Add(_poolObjects.IndexOf(returnedObject.GetComponent<PoolObject>()));
    }
}