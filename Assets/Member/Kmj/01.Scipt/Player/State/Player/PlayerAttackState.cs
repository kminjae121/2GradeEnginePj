
using Member.Kmj._01.Scipt.Entity.AttackCompo;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    private PlayerAttackCompo _attackCompo;
    private CharacterMovement _movement;

    public PlayerAttackState(Entity entity, int animationHash) : base(entity, animationHash)
    {
        _movement = entity.GetCompo<CharacterMovement>();
        _attackCompo = entity.GetCompo<PlayerAttackCompo>();
        
    }

    
    public override void Enter()
    {
        base.Enter();
        _movement.StopImmediately();
        _attackCompo.Attack();

        _movement.CanManualMovement = false;

        ApplyAttackData();
    }

  

    private void ApplyAttackData()
    {
        AttackDataSO currentAtkData = _attackCompo.GetCurrentAttackData();
        Vector3 playerDiretion = GetPlayerDirection();
        _player.transform.rotation = Quaternion.LookRotation(playerDiretion);

        Vector3 movement = playerDiretion * currentAtkData.movementPower;
    
    }

    private Vector3 GetPlayerDirection()
    {
        return _player.transform.forward;
    }

    public override void Exit()
    {
        _attackCompo.EndAttack();
        _movement.CanManualMovement = true;
        _movement.StopImmediately();
        base.Exit();
    }
    public override void Update()
    {
        base.Update();
        if (_isTriggerCall)
            _player.ChangeState("IDLE");
    }
}