using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

public interface IState
{
    IState On(Func<bool> checker, IState newState);
    ITransition CheckChangeState();

    void Enter();
    void LogicUpdate();
    void Exit();
}

public class State : IState
{
    protected StateContext _context;

    List<ITransition> _transitions = new List<ITransition>();

    public State(StateContext context)
    {
        _context = context;
    }

    public IState On(Func<bool> checker, IState nextState)
    {
        _transitions.Add(new Transition(checker, nextState));

        return this;
    }

    public ITransition CheckChangeState()
    {
        foreach (var transition in _transitions)
        {
            if (transition.Check())
            {
                return transition;
            }
        }
        return null;
    }

    public virtual void Enter()
    {

    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void Exit()
    {

    }
}

public class StateContext
{
    

    public GameObject gameObject { get; set; }
    public GameObject target { get; set; }
    public Transform spot { get; set; }

    public void TargetDown()
    {

    }
}

public class StateMove : State
{
    public StateMove(StateContext context) : base(context)
    {

    }

    public override void Enter()
    {
        Debug.Log("Enter MOVE");
    }

    public override void LogicUpdate()
    {

        if (_context.gameObject.transform.position != _context.spot.position)
        {
            Vector3 direction = _context.spot.position - _context.gameObject.transform.position;
            _context.gameObject.transform.Translate(direction * Time.deltaTime);
        }
    }

    public override void Exit()
    {
        Debug.Log("Exit MOVE");
    }
}

public class StateShoot : State
{
    private GameObject _target;
    private bool _canShoot;

    public StateShoot(StateContext context) : base(context)
    {

    }

    public override void Enter()
    {
        Debug.Log("Enter SHOOT");

        _canShoot = false;
        if (_context.target != null)
        {
            _target = _context.target;
            _canShoot = true;
        }
        //else
        //    Exit(context);
    }

    public override async void LogicUpdate()
    {
        //стреляем, пока не уничтожим цель

        if (_target != null && _canShoot)
        {
            _canShoot = false;
            await Task.Run(() => Shot());
        }
        else
        {

        }
    }

    private void Shot()
    {
        Thread.Sleep(5000);     //задержка для выстрела
        Debug.Log("SHOT");
        _canShoot = true;
        return;
    }

    public override void Exit()
    {
        Debug.Log("Exit SHOOT");

        //когда уничтожили цель, проверяем, не послали ли нас двигаться в другую точку
    }
}

public class StateSeek : State
{
    public StateSeek(StateContext context) : base(context)
    {

    }

    public override void Enter()
    {
        Debug.Log("Enter SEEK");
    }

    public override void LogicUpdate()
    {

        if (_context.target != null)
        {
            _context.TargetDown();
        }
    }

    public override void Exit()
    {
        Debug.Log("Exit SEEK");
    }
    
}