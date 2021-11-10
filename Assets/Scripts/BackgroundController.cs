using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [SerializeField] List<GameObject> tilemaps = new List<GameObject>();
    [SerializeField] Vector3 start_position;
    [SerializeField] float delay;
    [SerializeField] float tilemap_speed;

    bool can_launch;

    private void Awake()
    {
        can_launch = true;
    }

    private void Update()
    {
        if (can_launch)
        {
            StartCoroutine(Timer());
        }
    }

    IEnumerator Timer()
    {
        can_launch = false;
        yield return new WaitForSeconds(delay);
        Launch(Random.Range(0, tilemaps.Count));
        can_launch = true;
    }

    void Launch(int position)
    {
        GameObject random_tilemap = tilemaps[position];
        random_tilemap.transform.position = start_position;
        random_tilemap.GetComponent<TilemapController>().SetSpeed(tilemap_speed);
        random_tilemap.GetComponent<TilemapController>().Move();
    }
}
