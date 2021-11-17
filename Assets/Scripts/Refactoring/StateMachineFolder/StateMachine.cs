using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private IState _currentState;
    private List<IState> _states = new List<IState>();

    public IState Initialize(IState state)
    {
        _states.Add(state);

        return state;
    }

    public void ChangeState(IState newState)
    {
        if (_currentState != null)
        {
            _currentState.Exit();
        }
        _currentState = newState;
        _currentState.Enter();
    }

    public void Update()
    {
        ITransition transition = _currentState.CheckChangeState();

        if (transition != null)
        {
            _currentState.Exit();
            _currentState = transition.GetNextState();
            _currentState.Enter();
        }

        _currentState.LogicUpdate();
    }
}
