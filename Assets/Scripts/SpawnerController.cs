using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    [SerializeField] List<GameObject> enemy_prefabs = new List<GameObject>();

    [SerializeField] int enemies_min_count;
    [SerializeField] int enemies_max_count;

    [SerializeField] int enemies_min_timer;
    [SerializeField] int enemies_max_timer;

    [SerializeField] int spot_count;

    [SerializeField] Transform left_side;
    [SerializeField] Transform right_side;

    [SerializeField] GameObject spot;

    [SerializeField] UIController ui;

    int enemies_counter;

    List<GameObject> enemies = new List<GameObject>();
    List<GameObject> spots = new List<GameObject>();

    bool spawn = false;

    private void Awake()
    {
        Restart();
        SeedSpots();
    }

    private void Update()
    {
        if (spawn && enemies_counter > 0)
        {
            StartCoroutine(SpawnEnemy());
        }
        else if (enemies_counter <= 0 && enemies.Count == 0)
        {
            Victory();
        }
    }

    void SetCounter()
    {
        enemies_counter = Random.Range(enemies_min_count, enemies_max_count + 1);
    }

    void SeedSpots()
    {
        spots.Clear();

        Vector2 left = GetSpotPosition(left_side);
        Vector2 right = GetSpotPosition(right_side);

        FillSpots();

        float spacing = (right.x - left.x) / (spot_count + 1);

        for (int i = 0; i < spots.Count; i++)
        {
            spots[i].transform.position = new Vector3(left.x + spacing * (i + 1), left.y, 0);
        }
    }

    Vector2 GetSpotPosition(Transform spot)
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(spot.position);
        return new Vector2(pos.x, pos.y);
    }

    void FillSpots()
    {
        for (int i = 0; i < spot_count; i++)
        {
            GameObject new_spot = Instantiate(spot);
            new_spot.transform.SetParent(transform);
            spots.Add(new_spot);
        }
    }

    IEnumerator SpawnEnemy()
    {
        spawn = false;
        yield return new WaitForSeconds(Random.Range(enemies_min_timer, enemies_max_timer));

        int random_prefab = Random.Range(0, enemy_prefabs.Count);

        GameObject enemy = Instantiate(enemy_prefabs[random_prefab]);
        enemy.GetComponent<Enemy>().SetSpawner(gameObject);
        enemy.transform.SetParent(transform);
        enemy.transform.position = spots[Random.Range(0, spots.Count)].transform.position;
        enemies.Add(enemy);

        enemies_counter--;

        spawn = true;
    }

    public void RemoveEnemy(GameObject enemy)
    {
        enemies.Remove(enemy);
    }

    public void Restart()
    {
        DestroyAllEnemies();
        SetCounter();

        spawn = true;
    }

    void DestroyAllEnemies()
    {
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
        enemies.Clear();
    }

    void Victory()
    {
        ui.Victory();
    }

    public List<GameObject> GetEnemies()
    {
        return enemies;
    }
}
