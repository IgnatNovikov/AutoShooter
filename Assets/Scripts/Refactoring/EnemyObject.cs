using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObject : PoolObject, IMover
{
    [SerializeField] private float _speed;

    private bool _moves;

    private void Update()
    {
        Move(Vector3.up);
    }

    public void Move(Vector3 direction)
    {
        transform.Translate(Time.deltaTime * _speed * direction);
    }
}
