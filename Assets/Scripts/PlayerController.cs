using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Player parameters")]
    [SerializeField, Min(0)] private int _maxHealthPoints;
    private int _currentHp;

    [Header("Bullet parameters")]
    [SerializeField] private float _fireRate;
    [SerializeField] private float _fireRange;
    [SerializeField] private int _damage;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _bulletContainer;

    [Header("UI elements")]
    [SerializeField] private Text _hpText;

    private PoolController _bullets;
    private WaitForSeconds _shotDelay;
    private Transform _nearestEnemy;
    private GameManager _gameManager;
    private bool _canShoot;

    public void GetHit(int damage)
    {
        _currentHp -= damage;
        Redraw_HP();
        if (_currentHp <= 0)
        {
            _bullets.Refresh();
            _gameManager.Defeat();
        }
    }

    public void Restart()
    {
        _canShoot = true;
        _currentHp = _maxHealthPoints;

        _bullet.GetComponent<BulletObject>().SetParameters(_bulletSpeed, _damage, _fireRange);

        Redraw_HP();
    }

    private void Start()
    {
        _bullets = new PoolController(_bullet.GetComponent<BulletObject>(), Mathf.RoundToInt(_fireRange / _bullet.GetComponent<BulletObject>().Speed / _fireRate) + 1);

        _gameManager = GameManager.Instance;
        _shotDelay = new WaitForSeconds(_fireRate);

        Restart();
    }

    private void Update()
    {
        if (CheckNearestEnemy())
            StartCoroutine(Shooting());
    }

    private IEnumerator Shooting()
    {
        _canShoot = false;

        SetBullet(_bullets.GetObject());

        yield return _shotDelay;
        _canShoot = true;
    }

    private void SetBullet(GameObject bullet)
    {
        bullet.SetActive(true);
        bullet.transform.parent = _bulletContainer;
        bullet.transform.position = transform.position;
        bullet.transform.rotation = Quaternion.LookRotation(new Vector3(0, 0, 1), _nearestEnemy.position - transform.position);
        bullet.transform.GetComponent<BulletObject>().SetTarget(_nearestEnemy.transform);
    }

    private bool CheckNearestEnemy()
    {
        if (!_canShoot)
            return false;

        float min_range = float.MaxValue;

        List<GameObject> enemies = _gameManager.Spawner.GetEnemies();

        foreach (GameObject enemy in enemies)
        {
            float range = enemy.transform.position.y - transform.position.y;
            RaycastHit2D ray = Physics2D.Raycast(transform.position, enemy.transform.position - transform.position, _fireRange, _gameManager.enemyMask);

            if (ray.collider)
            {
                min_range = range;
                _nearestEnemy = enemy.transform;
            }
        }
        if (min_range < _fireRange)
        {
            return true;
        }

        return false;
    }

    private void Redraw_HP()
    {
        _hpText.text = _currentHp.ToString();
    }
}
