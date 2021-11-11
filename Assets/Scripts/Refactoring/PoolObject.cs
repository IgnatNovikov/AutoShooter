using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class PoolObject : MonoBehaviour
{
    public GameObject GameObject { get; private set; }

    protected void SetGameObject()
    {
        GameObject = this.gameObject;
    }

    public void ReturnToPool()
    {
        gameObject.SetActive(false);
    }
}
