using System;
using _01.Scipt.Player.Player;
using Blade.Core.StatSystem;
using Member.Kmj._01.Scipt.Entity.AttackCompo;
using UnityEngine;

public class PlayerRollCompo : MonoBehaviour, IEntityComponet, IAfterInit
{
    [SerializeField] private PlayerInputSO _inputReader;

    [SerializeField] private StatSO _rollStat;
    public float rollSpeed { get; set; }

    private Player _entity;
    public bool isRoll;

    private CharacterMovement _movement;

    private EntityAnimatorTrigger _triggerCompo;

    public void Initialize(Entity entity)
    {
        _entity = entity as Player;
        _triggerCompo = _entity.GetCompo<EntityAnimatorTrigger>();
        _inputReader.OnRollPressed += HandleRoll;
    }
    

    public void AfterInit()
    {
        rollSpeed = _rollStat.BaseValue;
        _movement = _entity.GetCompo<CharacterMovement>();
        _triggerCompo.OnRollStart += StartRoll;
        _triggerCompo.OnRollingStopping += SlowSpeed;
    }

    private void OnDestroy()
    {
        _inputReader.OnRollPressed -= HandleRoll;
        _triggerCompo.OnRollStart -= StartRoll;
        _triggerCompo.OnRollingStopping -= SlowSpeed;
    }

    public void HandleRoll()
    {
        if(_entity._isSkilling == false)
        {
            isRoll = true;
            _entity._movement.CanMove = false;
            _entity._isSkilling = true;
            _entity._attackCompo.IsAttack = false;
            _entity.ChangeState("ROLL");
        }
    }

    private void FixedUpdate()
    {
        if (isRoll)
        {
            _movement._rbcompo.AddForce(_entity.transform.forward * rollSpeed, ForceMode.Force);
        }
    }


    public void StartRoll()
    {
       // _movement._rbcompo.AddForce(_entity.transform.forward * rollSpeed, ForceMode.VelocityChange);
    }

    public void SlowSpeed()
    {
        _movement._rbcompo.linearDamping = 1.2f;
    }
}
