using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float movement_speed;

    private void Update()
    {
        transform.Translate(Vector2.up * Time.deltaTime * movement_speed);
    }
}
