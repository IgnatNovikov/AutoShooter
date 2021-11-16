using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public IState currentState;

    public GameObject _gameObject { get; private set; }
    public Transform _spot { get; private set; }
    public GameObject _target { get; private set; }

    public void Initialize(IState startingState, GameObject thisGameObject)
    {
        currentState = startingState;
        _gameObject = thisGameObject;
        startingState.Enter(this);
    }

    public void LogicUpdate()
    {
        currentState.LogicUpdate(this);
    }

    public void ChangeState(IState newState)
    {
        currentState.Exit(this);
        currentState = newState;
        currentState.Enter(this);
    }

    public void SetSpot(Transform newSpot)
    {
        _spot = newSpot;
    }

    public void SetTarget(GameObject target)
    {
        if (target != null)
            _target = target;
    }
}
