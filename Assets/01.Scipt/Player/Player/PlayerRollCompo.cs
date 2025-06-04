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

    public void Initialize(Entity entity)
    {
        _entity = entity as Player;
        _inputReader.OnRollPressed += HandleRoll;
    }

    public void HandleRoll()
    {
        _entity._movement.CanMove = false;
        _entity.ChangeState("ROLL");
    }

    public void AfterInit()
    {
        rollSpeed = _rollStat.BaseValue;
    }
}
