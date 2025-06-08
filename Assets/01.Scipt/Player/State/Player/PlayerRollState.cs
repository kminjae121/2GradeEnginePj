using Member.Kmj._01.Scipt.Entity.AttackCompo;
using UnityEngine;

public class PlayerRollState : PlayerState
{

    public PlayerRollState(Entity entity, int animationHash) : base(entity, animationHash)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        _player._collider.enabled = false;
        
        _player._movement._rbcompo.useGravity = false;
    }

    public override void Update()
    {
        if (_isTriggerCall)
        {
            _player._movement._rbcompo.linearVelocity = Vector3.zero;
            _player._collider.enabled = true;
            _player._movement._rbcompo.useGravity = true;
            _player._movement.CanMove = true;
            _player.ChangeState("IDLE");
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}