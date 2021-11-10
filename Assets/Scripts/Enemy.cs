using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int health_points;
    [SerializeField] int damage;

    [SerializeField] GameObject explosion;

    SpawnerController spawner;

    public void SetSpawner(GameObject parent)
    {
        spawner = parent.GetComponent<SpawnerController>();
    }

    public void GetHit(int damage)
    {
        health_points -= damage;
        if (health_points <= 0)
            Death();
    }

    void Death()
    {
        GameObject explosion = Instantiate(this.explosion);
        explosion.transform.position = transform.position;

        spawner.RemoveEnemy(gameObject);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().GetHit(damage);
            Death();
        }
    }
}
