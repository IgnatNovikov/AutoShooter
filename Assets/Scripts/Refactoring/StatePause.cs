using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePause : State
{
    public void Next()
    {
        Time.timeScale = 1f;
    }
}
