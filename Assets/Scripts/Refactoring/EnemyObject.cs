using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObject : PoolObject, IMover
{
    [SerializeField] private float _speed;

    private bool _moves;

    private void Update()
    {
        if (_moves)
            Move(Vector3.down);
    }

    public void Move(Vector3 direction)
    {
        transform.Translate(Time.deltaTime * _speed * direction);
    }

    public void Launch(Transform target, float speed)
    {
        _moves = true;
    }
}
