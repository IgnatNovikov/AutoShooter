using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObject : PoolObject, IMover
{
    [SerializeField] private float _speed;
    [SerializeField] private int _health;
    [SerializeField] private int _damage;

    private int _currentHealth;
    private Vector3 _direction;
    private RaycastHit2D _ray;

    public float Speed
    {
        get => _speed;
    }

    public void GetHit(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
            Death();
    }

    public void Move(Vector3 direction)
    {
        transform.Translate(Time.deltaTime * _speed * direction);
    }

    public void SetAlive()
    {
        _isAlive = true;
    }

    private void Start()
    {
        _currentHealth = _health;
        _direction = Vector3.up;
    }

    private void Update()
    {
        if (_isAlive)
        {
            if (Hit())
            {
                _ray.collider.gameObject.GetComponent<PlayerController>().GetHit(_damage);
                Death();
            }

            Move(_direction);
        }
    }

    private bool Hit()
    {
        _ray = Physics2D.Raycast(transform.position, _direction, Time.deltaTime * 50, GameManager.Instance.playerMask);
        if (_ray)
            return true;

        return false;
    }

    private void Death()
    {
        _isAlive = false;
        _currentHealth = _health;
        ReturnToPool(gameObject);
    }
}
