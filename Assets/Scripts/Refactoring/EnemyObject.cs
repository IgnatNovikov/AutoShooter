using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObject : PoolObject
{
    private void Update()
    {
        if (_moves)
            Move(Vector3.down);
    }

    public override void Launch(Transform target, float speed)
    {
        _moves = true;
    }

    public override void Death()
    {
        
    }
}
