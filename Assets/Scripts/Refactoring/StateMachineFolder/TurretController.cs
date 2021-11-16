using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    private StateMachine _stateMachine;

    // Start is called before the first frame update
    void Start()
    {
        _stateMachine = new StateMachine();
        _stateMachine.Initialize(new StateSeek(), gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        _stateMachine.LogicUpdate();
    }
}
