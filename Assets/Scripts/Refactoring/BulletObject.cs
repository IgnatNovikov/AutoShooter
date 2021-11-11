using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObject : PoolObject
{
    private static int _damage;
    private static float _maxRange;
    private Transform _target;
    private Vector3 _lastDirection;
    private static WaitForSeconds _lifeTime;

    private void Start()
    {
        SetSpeed(0);
    }

    private void Update()
    {
        if (_target != null)
        {
            _lastDirection = _target.position - transform.position;
        }

        if (_moves)
        {
            Move(_lastDirection);
            transform.rotation = Quaternion.LookRotation(Vector3.forward, _lastDirection);
        }
    }

    public override void Launch(Transform target, float speed)
    {
        gameObject.SetActive(true);
        
        _speed = speed;
        StartCoroutine(Counter(target));
    }

    private IEnumerator Counter(Transform target)
    {
        _lifeTime = CalculateCounterFromDistance();
        Debug.Log(_maxRange / _speed);

        _target = target;
        _moves = true;

        yield return _lifeTime;

        if (this != null)
        {
            ReturnToPool();
        }
    }

    private WaitForSeconds CalculateCounterFromDistance()
    {
        return new WaitForSeconds(_maxRange / _speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy other = collision.gameObject.GetComponent<Enemy>();

        if (other != null)
        {
            other.GetHit(_damage);
            ReturnToPool();
        }
    }
}
