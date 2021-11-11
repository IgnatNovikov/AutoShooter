using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollidable
{
    public void OnCollisionEnter2D(Collision2D collision);
}
