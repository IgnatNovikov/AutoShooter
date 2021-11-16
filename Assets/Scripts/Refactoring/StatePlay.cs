using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePlay : State
{
    public void Next()
    {
        Time.timeScale = 0f;
    }
    /*
    public class FallState : State<Player>
    {
        float _baseLandingTime = 0.15f;
        float _hardLandingTime = 0.9f;

        float _lastVelocity = 0;
        float _waitTime;
        bool _landing = false;
        bool _fastFall = false;

        public FallState(Player stateContext) : base(stateContext) { }

        public override void Enter()
        {
            base.Enter();
            GameLogger.Log(LogLayer.PlayerStateMachine, "Enter FallState");

            _fastFall = false;
            _landing = false;
            _waitTime = 0;
            if (_context.RigidBody.velocity.y > 0)
            {
                _context.SetRBVelocity(new Vector2(_context.RigidBody.velocity.x, 0));
            }
            _context.RigidBody.isKinematic = false;
            _context.RigidBody.interpolation = RigidbodyInterpolation2D.Interpolate;
            _context.SetAnimationState(PlayerStateId.Fall);
            _context.TryUse = false;
            _context.IgnoreEnemiesCollision(true);
            _context.PlayerAnimator.ResetTrigger(_context.FinishStateParametrId);
            _context.StateAutoAttack = false;
            _lastVelocity = 0f;
        }

        public override void Exit()
        {
            base.Exit();
            GameLogger.Log(LogLayer.PlayerStateMachine, "Exit FallState");
            _context.IgnoreEnemiesCollision(false);
            _context.JumpOff = false;
            _context.PlayerAnimator.ResetTrigger(_context.FinishStateParametrId);
            _context.RigidBody.interpolation = RigidbodyInterpolation2D.Extrapolate;
        }

        public override void Update()
        {
            base.Update();

            if (_waitTime > 0)
            {
                if (_landing && _waitTime > 0)
                {
                    _waitTime -= Time.deltaTime;
                }

                if (_landing && _waitTime <= 0 && !_context.IsGrounded)
                {
                    _context.SetAnimationState(PlayerStateId.Fall);
                    _context.PlayerAnimator.ResetTrigger(_context.FinishStateParametrId);
                }
            }

            if (!_landing || !_fastFall)
            {
                _context.Velocity = _context.GetDirectionMove() != 0 ? _context.Speed : 0;
                _context.ApplyVelocity();
            }
            else
            {
                _context.SetRBVelocityX(0);
            }

            float newVelocity = Mathf.Abs(_context.RigidBody.velocity.y);

            if (_lastVelocity < newVelocity)
                _lastVelocity = newVelocity;

            if (_context.IsGrounded && !_landing)
            {
                _landing = true;
                _context.PlayerAnimator.ResetTrigger(_context.StartStateParametrId);
                _context.PlayerAnimator.SetTrigger(_context.FinishStateParametrId);
                _waitTime = _fastFall ? _hardLandingTime : _baseLandingTime;
                _context.ApplyFallDamage(_lastVelocity);
                _context.PlaySound(_fastFall ? PlayerSoundType.CrashLanding : PlayerSoundType.Landing);
            }

            if (!_fastFall && _context.IsWillFallDamage())
            {
                _fastFall = true;
                _context.SetAnimationState(PlayerStateId.FastFall);
            }

            _context.PlayerAnimator.SetFloat(_context.JumpSideBlendParametrId, _context.GetDirectionMove() != 0 ? 1.0f : 0.0f);
        }

        public override bool Ready()
        {
            return _landing && _waitTime <= 0;
        }

        public bool HardFalling()
        {
            return _fastFall;
        }
    }
    */
}