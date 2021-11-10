using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    float bullet_speed;
    int damage;

    GameObject target;
    Vector3 last_direction;

    bool fire;

    private void Awake()
    {
        fire = false;
        last_direction = Vector2.up;
    }

    private void Update()
    {
        if (target != null)
        {
            last_direction = target.transform.position - transform.position;
        }

        if (fire)
        {
            transform.Translate(last_direction.normalized * Time.deltaTime * bullet_speed);
            transform.rotation = Quaternion.LookRotation(new Vector3(0, 0, 1), last_direction);
        }
    }

    public IEnumerator Counter(int damage, float speed, float max_range, GameObject target)
    {
        bullet_speed = speed;
        this.damage = damage;

        this.target = target;
        last_direction = target.transform.position - transform.position;
        fire = true;
        yield return new WaitForSeconds(max_range / bullet_speed);

        if (this != null)
        {
            Death();
        }
    }

    void Death()
    {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;
        if (other.tag == "Enemy")
        {
            other.GetComponent<Enemy>().GetHit(damage);
            Death();
        }
    }
}
