using Member.Kmj._01.Scipt.Entity.AttackCompo;
using UnityEngine;

public class MousePlayerSheldState : EntityState
{
    private Player _player;
    private MouseBarrerSkill _skillCompo;

    private float time;
    public MousePlayerSheldState(Entity entity, int animationHash) : base(entity, animationHash)
    {
        _player = entity as Player;
        _skillCompo = entity.GetComponentInChildren<MouseBarrerSkill>();
    }

    public override void Enter()
    {
        base.Enter();
        time = 0;
        
        _player._barrerSkill.isPalling = true;
        _player.ChangeState("IDLE");
        _player._movement.StopImmediately();
    }

    public override void Update()
    {
        base.Update();

        time += Time.deltaTime;
        
        _player._movement.CanMove = false;

        if (time >= 10)
            _skillCompo.HandleBarrierCanceled();
        else if (time >= 0.8f)
            _player._barrerSkill.isPalling = false;

    }

    public override void Exit()
    {
        // _energyCompo.CancelSkill();
        _player._isSkilling = false;
        _player._movement.CanMove = true;
        base.Exit();
    }
}
