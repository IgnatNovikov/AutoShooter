using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void Enter(StateMachine context);

    void LogicUpdate(StateMachine context);

    void Exit(StateMachine context);
}

public class StateMove : IState
{
    public void Enter(StateMachine context)
    {
        Debug.Log("Enter MOVE");
    }

    public void LogicUpdate(StateMachine context)
    {
        Vector3 direction = context._spot.position - context._gameObject.transform.position;
        if (context._gameObject.transform.position != context._spot.position)
            context._gameObject.transform.Translate(direction * Time.deltaTime);
        else
            Exit(context);
    }

    public void Exit(StateMachine context)
    {
        Debug.Log("Exit MOVE");
        context.ChangeState(new StateSeek());
    }
}

public class StateShoot : MonoBehaviour, IState
{
    private GameObject _target;

    public void Enter(StateMachine context)
    {
        Debug.Log("Enter SHOOT");

        if (context._target != null)
        {
            _target = context._target;
        }
        else
            Exit(context);
    }

    public void LogicUpdate(StateMachine context)
    {
        //��������, ���� �� ��������� ����

        if (_target == null)
        {
            Exit(context);
        }
    }

    public void Exit(StateMachine context)
    {
        Debug.Log("Exit SHOOT");

        //����� ���������� ����, ���������, �� ������� �� ��� ��������� � ������ �����

        if (context._spot == null)
            context.ChangeState(new StateSeek());
        else
            context.ChangeState(new StateMove());
    }
}

public class StateSeek : IState
{
    public void Enter(StateMachine context)
    {
        Debug.Log("Enter SEEK");

        if (context._target != null)    //���� � ��� ��� ���� ����, �� ������� � ��������
            Exit(context);
    }

    public void LogicUpdate(StateMachine context)
    {

        if (context._target != null)
            Exit(context);
    }

    public void Exit(StateMachine context)
    {
        Debug.Log("Exit SEEK");

        if (context._target != null)
            context.ChangeState(new StateShoot());
        else if (context._spot != null)
            context.ChangeState(new StateMove());
        else
            Enter(context);
    }
}