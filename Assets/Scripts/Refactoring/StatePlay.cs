using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePlay : State
{
    public override void Next()
    {
        Time.timeScale = 0f;
        GameManager.Instance.state = new StatePause();
    }
}
