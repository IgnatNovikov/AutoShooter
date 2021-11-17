using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITransition
{
    bool Check();
    IState GetNextState();
}

public class Transition : ITransition
{
    private Func<bool> _checker;
    private IState _nextState;

    public Transition(Func<bool> checker, IState nextState)
    {
        _checker = checker;
        _nextState = nextState;
    }

    public bool Check()
    {
        return _checker.Invoke();
    }

    public IState GetNextState()
    {
        return _nextState;
    }
}

/*
public class ToSeekTransition : Transition
{
    public override void Initialize(Func<bool> func)
    {

    }
}

public class ToMoveTransition : Transition
{
    public Func<bool> condition { get; }
    public State nextState { get; }

    public void Initialize(Func<bool> func)
    {

    }

    public void On()
    {

    }
}
*/