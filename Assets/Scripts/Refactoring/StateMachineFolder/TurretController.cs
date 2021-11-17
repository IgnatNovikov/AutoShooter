using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    private StateMachine _stateMachine;
    private StateContext _stateContext;

    //StateShoot stateShoot;

    // Start is called before the first frame update
    void Start()
    {
        _stateMachine = new StateMachine();
        _stateContext = new StateContext();

        _stateContext.target = GameManager.Instance.Player.gameObject;

        IState stateSeek = _stateMachine.Initialize(new StateSeek(_stateContext));
        IState  stateShoot =_stateMachine.Initialize(new StateShoot(_stateContext));
        IState stateMove = _stateMachine.Initialize(new StateMove(_stateContext));

        stateSeek.On(IsSpot, stateMove)
            .On(IsTargetFound, stateShoot);
        stateMove.On(IsSpotted, stateSeek);
        stateShoot.On(IsSpot, stateMove)
            .On(IsDestroyed, stateSeek);

        _stateMachine.ChangeState(stateSeek);
    }

    // Update is called once per frame
    void Update()
    {
        _stateMachine.Update();
    }

    private bool IsSpot()
    {
        if (_stateContext.spot != null)
            return true;

        return false;
    }

    private bool IsSpotted()
    {
        if (_stateContext.target.transform.position == transform.position)
            return true;

        return false;
    }

    private bool IsTargetFound()
    {
        if (_stateContext.target != null)
            return true;

        return false;
    }

    private bool IsDestroyed()
    {
        if (_stateContext.target == null)
            return true;

        return false;
    }
}
