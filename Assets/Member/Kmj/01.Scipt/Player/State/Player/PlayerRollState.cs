using System.Collections;
using Member.Kmj._01.Scipt.Entity.AttackCompo;
using UnityEngine;

public class PlayerRollState : PlayerState
{
    private CharacterMovement _movement;
    private bool _isRolling;
    private Vector3 _rollingDirection;

    public PlayerRollState(Entity entity, int animationHash) : base(entity, animationHash)
    {
        _movement = entity.GetCompo<CharacterMovement>();
    }

    public override void Enter()
    {
        _movement._velocity = Vector3.zero;
        _movement.IsRolling = true;
        base.Enter();
       
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (_isTriggerCall)
        {
            _movement.IsRolling = false;
            _player.ChangeState("IDLE");
        }
    }
}
