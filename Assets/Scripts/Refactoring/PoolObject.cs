using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class PoolObject : MonoBehaviour
{
    protected float _speed;
    protected Vector3 _direction;
    protected bool _moves;

    public void ReturnToPool()
    {
        gameObject.SetActive(false);
    }

    public void Move(Vector3 direction)
    {
        transform.Translate(Time.deltaTime * _speed * _direction.normalized);
    }

    abstract public void Launch(Transform target, float speed);

    protected void SetSpeed(float speed)
    {
        if (speed > 0)
            _speed = speed;
        else
            _speed = 0;
    }

    abstract public void Death();
}
