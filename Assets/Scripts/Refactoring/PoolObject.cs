using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class PoolObject : MonoBehaviour
{
    public void ReturnToPool()
    {
        gameObject.SetActive(false);
    }
}
