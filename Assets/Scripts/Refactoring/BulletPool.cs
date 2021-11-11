using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : PoolController
{
    public BulletPool(GameObject prefab, Transform parent, float speed, int damage, float maxRange, int count)
    {
        _poolObjects = new List<PoolObject>();

        for (int i = 0; i < count; i++)
        {
            AddObject(prefab, parent);
        }
    }

    public override void AddObject(GameObject poolObject, Transform parentTransform)
    {
        GameObject newObject = GameObject.Instantiate(poolObject.gameObject);
        newObject.transform.position = parentTransform.position;
        newObject.transform.parent = parentTransform;

        _poolObjects.Add(newObject.GetComponent<PoolObject>());
    }

    public override void AddObject(GameObject poolObject, Transform parentTransform, Transform spawnSpot)
    {
        GameObject newObject = GameObject.Instantiate(poolObject);

        _poolObjects.Add(newObject.GetComponent<PoolObject>());
    }

    public override void AddMultipleObjects(PoolObject poolObject, Transform parentTransform, Transform spawnSpot, int count)
    {
        
    }
}
