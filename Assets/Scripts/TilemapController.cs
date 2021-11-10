using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapController : MonoBehaviour
{
    float speed;

    bool move;

    Renderer tilemap_renderer;

    private void Awake()
    {
        tilemap_renderer = GetComponent<Renderer>();
        tilemap_renderer.enabled = false;

        speed = 0;
    }

    void Update()
    {
        if (move)
        {
            transform.Translate(Vector2.down * Time.deltaTime * speed);
        }
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void Stop()
    {
        move = false;
        tilemap_renderer.enabled = false;
    }

    public void Move()
    {
        move = true;
        tilemap_renderer.enabled = true;
    }
}
