using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePause : State
{
    public override void Next()
    {
        Time.timeScale = 1f;
        GameManager.Instance.state = new StatePlay();
    }
}
