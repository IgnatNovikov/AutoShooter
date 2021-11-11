using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : PoolController
{
    private List<BulletObject> _bullets;

    public BulletPool(GameObject prefab, Transform parent, float speed, int damage, float maxRange, int count)
    {
        _prefab = prefab;
        _bullets = new List<BulletObject>();

        for (int i = 0; i < count; i++)
        {
            AddObject(_prefab);
        }
    }

    protected override PoolObject GetObject(Transform parentTransform, Transform spawnSpot)
    {
        return new BulletObject();
    }

    protected override PoolObject GetFree()
    {
        foreach (BulletObject bullet in _bullets)
        {
            if (!bullet.gameObject.activeInHierarchy)
                return bullet;
        }

        AddObject(_prefab);

        return _bullets[_bullets.Count - 1];
    }

    public override void AddObject(GameObject poolObject)
    {
        GameObject newObject = GameObject.Instantiate(poolObject);
    }

    public override void AddMultipleObjects(PoolObject poolObject, int count)
    {
        for (int i = 0; i < count; i++)
        {
            AddObject(poolObject.gameObject);
        }
    }
}
