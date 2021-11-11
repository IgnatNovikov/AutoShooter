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
        //Debug.Log(gameObject.name);
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
            //transform.rotation = Quaternion.LookRotation(new Vector3(0, 0, 1), _lastDirection);
            transform.rotation = Quaternion.LookRotation(Vector3.forward, _lastDirection);
        }
    }

    public IEnumerator Counter(Transform target)
    {
        gameObject.SetActive(true);

        _target = target;
        //_lastDirection = _target.position - transform.position;
        _moves = true;

        yield return _lifeTime;

        if (this != null)
        {
            Death();
        }
    }

    public override void Death()
    {
        _moves = false;

        ReturnToPool();
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
            Death();
        }
    }
}
