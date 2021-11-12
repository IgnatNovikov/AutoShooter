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

    private void Start()
    {
        _moves = true;
    }

    private void Update()
    {
        if (_target != null)
        {
            _lastDirection = _target.position - transform.position;
        }

        if (_moves)
        {
            if (Hit())
            {
                Debug.Log(_ray.collider.gameObject.name);
            }
            Move(_lastDirection);
            transform.rotation = Quaternion.LookRotation(Vector3.forward, _lastDirection);
        }
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

    public void Launch(Transform target, float speed)
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

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private bool Hit()
    {
        _ray = Physics2D.Raycast(transform.position, _target.position - transform.position, Time.deltaTime * _speed);
        Debug.DrawLine(transform.position, transform.position + (Time.deltaTime * 150 * (_target.position - transform.position).normalized));
        if (_ray)
            return true;

        return false;
    }
}
