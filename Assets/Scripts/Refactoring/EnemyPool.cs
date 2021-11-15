using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyPool : MonoBehaviour
{
    [Header("Spawn parameters")]
    [SerializeField] private GameObject _enemyPrefab;

    [SerializeField] private int _enemiesMinCount;
    [SerializeField] private int _enemiesMaxCount;
    [SerializeField] private int _enemiesMinTimer;
    [SerializeField] private int _enemiesMaxTimer;

    [Header("Spot parameters")]
    [SerializeField] private int _spotCount;
    [SerializeField] private GameObject _spot;
    [SerializeField] private Transform _leftSide;
    [SerializeField] private Transform _rightSide;
    
    private GameManager _gameManager;
    private int _enemiesCounter;
    private List<GameObject> _spots = new List<GameObject>();
    private PoolController _enemies;

    public List<GameObject> GetEnemies()
    {
        return _enemies.GetAllActive();
    }

    public void Restart()
    {
        _enemiesCounter = Random.Range(_enemiesMinCount, _enemiesMaxCount);
        _enemies.Refresh();

        StartCoroutine(SpawnEnemy());
    }

    private void Start()
    {
        _gameManager = GameManager.Instance;
        SeedSpots();

        _enemies = new PoolController(_enemyPrefab.GetComponent<EnemyObject>(), Mathf.RoundToInt(CalculatePoolSize() / _enemiesMinTimer + 1));
        _enemiesCounter = Random.Range(_enemiesMinCount, _enemiesMaxCount);

        StartCoroutine(SpawnEnemy());
    }

    private float CalculatePoolSize()
    {
        float distance = _spots[0].transform.position.y - _gameManager.Player.transform.position.y;
        return distance / _enemyPrefab.GetComponent<EnemyObject>().Speed;
    }

    private void SeedSpots()
    {
        _spots.Clear();

        Vector2 left = GetSpotPosition(_leftSide);
        Vector2 right = GetSpotPosition(_rightSide);

        float spacing = (right.x - left.x) / (_spotCount + 1);
        Vector3 space = new Vector3(left.x, left.y, 0);

        for (int i = 0; i < _spotCount; i++)
        {
            GameObject newSpot = Instantiate(_spot);
            newSpot.transform.SetParent(transform);
            space.x = left.x + spacing * (i + 1);
            newSpot.transform.position = space;
            _spots.Add(newSpot);
        }
    }

    private Vector2 GetSpotPosition(Transform spot)
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(spot.position);
        return new Vector2(pos.x, pos.y);
    }

    private IEnumerator SpawnEnemy()
    {
        GameObject enemy = _enemies.GetObject();
        enemy.transform.SetParent(transform);
        enemy.transform.position = _spots[Random.Range(0, _spots.Count)].transform.position;
        enemy.SetActive(true);
        enemy.GetComponent<EnemyObject>().SetAlive();

        yield return new WaitForSeconds(Random.Range(_enemiesMinTimer, _enemiesMaxTimer));

        _enemiesCounter--;
        if (_enemiesCounter <= 0)
        {
            _gameManager.Victory();
        }
        else
            StartCoroutine(SpawnEnemy());
    }
}
