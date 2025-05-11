using Member.Kmj._01.Scipt.Entity.AttackCompo;
using UnityEngine;

public class PlayerDieState : PlayerState
{
    public PlayerDieState(Entity entity, int animationHash) : base(entity, animationHash)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _player._movement.StopImmediately();
    }

    public override void Update()
    {
        base.Update();
        if (_isTriggerCall)
            Exit();
    }
    public override void Exit()
    {
        _player.gameObject.SetActive(false);
        base.Exit();
    }
}
