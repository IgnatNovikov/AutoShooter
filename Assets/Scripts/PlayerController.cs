using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField, Min(0)] int health_points;
    int current_hp;

    [SerializeField] float fire_rate;
    [SerializeField] float fire_range;
    [SerializeField] int damage;
    [SerializeField] float bullet_speed;

    [SerializeField] GameObject bullet;
    [SerializeField] Transform bullet_container;

    [SerializeField] string enemy_mask;

    [SerializeField] SpawnerController spawner;

    [SerializeField] Text hp_text;
    [SerializeField] UIController ui;

    GameObject nearest_enemy;

    bool can_shoot;

    private void Awake()
    {
        Restart();
    }

    private void Update()
    {
        if (CheckNearestEnemy())
            StartCoroutine(Shooting());
    }

    IEnumerator Shooting()
    {
        can_shoot = false;

        GameObject new_bullet = Instantiate(bullet);
        SetBullet(new_bullet);

        yield return new WaitForSeconds(fire_rate);
        can_shoot = true;
    }

    void SetBullet(GameObject bullet)
    {
        bullet.transform.parent = bullet_container;
        bullet.transform.position = transform.position;
        bullet.transform.rotation = Quaternion.LookRotation(new Vector3(0, 0, 1), nearest_enemy.transform.position - transform.position);

        StartCoroutine(bullet.GetComponent<BulletController>().Counter(damage, bullet_speed, fire_range, nearest_enemy));
    }

    bool CheckNearestEnemy()
    {
        if (!can_shoot)
            return false;

        float min_range = float.MaxValue;
        int layerMask = (1 << LayerMask.NameToLayer(enemy_mask));

        List<GameObject> enemies = spawner.GetEnemies();
        if (enemies.Count > 0)
        {
            foreach (GameObject enemy in enemies)
            {
                float range = enemy.transform.position.y - transform.position.y;
                RaycastHit2D ray = Physics2D.Raycast(transform.position, enemy.transform.position - transform.position, fire_range, layerMask);
                
                if (ray.collider != null)
                {
                    if (ray.collider.gameObject == enemy && range < min_range)
                    {
                        min_range = range;
                        nearest_enemy = enemy;
                    }
                }
            }
        }
        if (min_range < fire_range)
        {
            return true;
        }

        return false;
    }

    public void GetHit(int damage)
    {
        current_hp -= damage;
        Redraw_HP();
        if (current_hp <= 0)
        {
            ui.Defeat();
        }
    }

    public void Restart()
    {
        can_shoot = true;
        current_hp = health_points;
        Redraw_HP();
    }

    void Redraw_HP()
    {
        hp_text.text = "HEALTH: " + current_hp;
    }
}
