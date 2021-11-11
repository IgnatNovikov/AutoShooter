using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : PoolController
{
    private List<EnemyObject> _enemies;

    public EnemyPool(GameObject prefab, Transform parent, float speed, int damage, float maxRange, int count)
    {
        _enemies = new List<EnemyObject>();

        for (int i = 0; i < count; i++)
        {
            AddObject(prefab);
        }
    }

    protected override PoolObject GetObject(Transform parentTransform, Transform spawnSpot)
    {
        return new BulletObject();
    }

    protected override PoolObject GetFree()
    {
        foreach (EnemyObject enemy in _enemies)
        {
            if (!enemy.gameObject.activeInHierarchy)
                return enemy;
        }

        AddObject(_prefab);

        return _enemies[_enemies.Count - 1];
    }

    public override void AddObject(GameObject poolObject)
    {
        
    }

    public override void AddMultipleObjects(PoolObject poolObject, int count)
    {
        
    }
}
