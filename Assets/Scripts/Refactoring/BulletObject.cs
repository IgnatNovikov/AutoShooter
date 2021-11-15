using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObject : PoolObject, IMover
{
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;
    [SerializeField] private float _maxRange;

    private Transform _target;
    private Vector3 _lastDirection;
    private static WaitForSeconds _lifeTime;
    private RaycastHit2D _ray;

    private bool _moves;

    public float Speed
    {
        get => _speed;
    }

    public void SetParameters(float speed, int damage, float maxRange)
    {
        if (speed > 0)
        {
            _speed = speed;
        }

        if (damage > 0)
        {
            _damage = damage;
        }

        if (maxRange > 0)
        {
            _maxRange = maxRange;
        }
    }

    public void Move(Vector3 direction)
    {
        transform.Translate(Time.deltaTime * _speed * direction.normalized);
    }

    public void SetTarget(Transform target)
    {
        _isAlive = true;
        _target = target;
        StartCoroutine(Counter());
    }

    private void Start()
    {
        _isAlive = true;
    }

    private void Update()
    {
        if (_isAlive)
        {
            if (_target.gameObject.activeInHierarchy)
            {
                _lastDirection = _target.position - transform.position;
            }

            if (Hit())
            {
                _isAlive = false;
                _ray.collider.gameObject.GetComponent<EnemyObject>().GetHit(_damage);
                StopAllCoroutines();
                ReturnToPool(gameObject);
            }
            Move(_lastDirection);
            transform.rotation = Quaternion.LookRotation(Vector3.forward, _lastDirection);
        }
    }

    private IEnumerator Counter()
    {
        _lifeTime = CalculateCounterFromDistance();
        _isAlive = true;

        yield return _lifeTime;

        if (this != null)
        {
            ReturnToPool(gameObject);
        }
    }

    private WaitForSeconds CalculateCounterFromDistance()
    {
        return new WaitForSeconds(_maxRange / _speed);
    }

    private bool Hit()
    {
        _ray = Physics2D.Raycast(transform.position, _target.position - transform.position, Time.deltaTime * _speed, GameManager.Instance.enemyMask);
        if (_ray)
            return true;

        return false;
    }
}
