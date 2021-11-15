using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class PoolObject : MonoBehaviour
{
    public System.Action<GameObject> onReturn;

    protected bool _isAlive;

    public void ReturnToPool(GameObject returned)
    {
        onReturn?.Invoke(returned);
        gameObject.SetActive(false);
    }
}
