using Member.Kmj._01.Scipt.Entity.AttackCompo;
using UnityEngine;

public class PlayerFuryState : PlayerState
{
    public PlayerFuryState(Entity entity, int animationHash) : base(entity, animationHash)
    {
    }


    public override void Enter()
    {
        base.Enter();
        _player._movement._rbcompo.linearVelocity = Vector3.zero;
        _player._movement.StopImmediately();
        _player._movement.CanMove = false;
        _player._attackCompo.atkDamage += 35;
        _player._skillCompo.skillDamage += 35;
    }

    public override void Update()
    {
        base.Update();
        if (_isTriggerCall)
        {
            _player.ChangeState("IDLE");
        }
    }

    public override void Exit()
    {
        _player._movement.CanMove = true;
        base.Exit();
    }
}
