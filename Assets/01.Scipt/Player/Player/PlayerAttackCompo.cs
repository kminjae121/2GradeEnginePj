using System;
using System.Collections;
using System.Numerics;
using _01.Scipt.Blade.Combat;
using _01.Scipt.Player.Player;
using a;
using Blade.Combat;
using Member.Kmj._01.Scipt.Entity.AttackCompo;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class PlayerAttackCompo : MonoBehaviour, IEntityComponet
{
    public AttackDataSO[] attackDataList;

    [SerializeField] private float comboWindow;

    [SerializeField] private LayerMask _whatIsEnemy;

   
    [SerializeField] private EntityStat _stat;

    [SerializeField] private Vector3 _boxsize;

    [field: SerializeField] public float MaxHoldTime { get; set; }
    [field: SerializeField] public bool IsAttack { get; set; }

    public bool useMouseDirection;

    private readonly int _attackSpeedHash = Animator.StringToHash("ATTACK_SPEED");
    private readonly int _comboCounterHash = Animator.StringToHash("COMBO_COUNTER");

    private float _attackSpeed = 0.3f;

    private Coroutine _chargeRoutine;
    private Entity _entity;
    private EntityAnimator _entityAnimator;
    private float _lastAttackTime;
    private Player _player;

    private EntityAnimatorTrigger _triggerCompo;
    
    private EntityVFX _vfxCompo;
    private float attackHoldTime;
    [SerializeField] private OverlapDamageCaster damageCaster;

    public int ComboCounter { get; set; }
    
    [field: SerializeField] public Transform FinalAttackEffect { get; set; }


    public float AttackSpeed
    {
        get => _attackSpeed;
        set
        {
            _attackSpeed = value;
            _entityAnimator.SetParam(_attackSpeedHash, _attackSpeed);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, _boxsize);
        Gizmos.color = Color.white;
    }

    public void Initialize(Entity entity)
    {
        _entity = entity;
        _player = entity as Player;
        _entityAnimator = entity.GetCompo<EntityAnimator>();
        
        
        _vfxCompo = entity.GetCompo<EntityVFX>();
        AttackSpeed = 1.6f;
        //damageCast.InitCaster(_entity);
        _triggerCompo = entity.GetCompo<EntityAnimatorTrigger>();
        _triggerCompo.OnSwingAttackTrigger += HandleSwing;
        _triggerCompo.OnAttackAnimEnd += EndAttack;
        _triggerCompo.OnAttackTriggerEnd += HandleDamageCasterTrigger;
        _triggerCompo.OnAttackCancel += AttackCancel;
        _triggerCompo.OnAttackVFXTrigger += HandleAttackVFXTrigger;
        _triggerCompo.OnAttackFinalVFXTrigger += HandleFinalAttackTrigger;
        _player.PlayerInput.OnChargeAttackPressed += StartCharge;
        _player.PlayerInput.OnChargeAttackCanceled += StopCharge;
        _triggerCompo.LastAttackEffectEndTrigger += HandleStopFinalAttackTrigger;
    }


    private void OnDisable()
    {
        _triggerCompo.OnAttackVFXTrigger -= HandleAttackVFXTrigger; 
        _triggerCompo.OnAttackAnimEnd -= EndAttack;
        _triggerCompo.OnAttackCancel -= AttackCancel;
        _triggerCompo.LastAttackEffectEndTrigger -= HandleStopFinalAttackTrigger;
        _triggerCompo.OnAttackTriggerEnd -= HandleDamageCasterTrigger;
        
        _triggerCompo.OnAttackFinalVFXTrigger -= HandleFinalAttackTrigger;
        _player.PlayerInput.OnChargeAttackPressed -= StartCharge;
        _player.PlayerInput.OnChargeAttackCanceled -= StopCharge;
    }
    
    
    private void HandleAttackVFXTrigger() 
    {
        _vfxCompo.PlayVfx($"AttackVFX{ComboCounter}", Vector3.zero, Quaternion.identity);
    }
    private void HandleFinalAttackTrigger()
    {
        _vfxCompo.PlayVfx($"FinalAttack", FinalAttackEffect.position, Quaternion.identity);
    }

    private void HandleStopFinalAttackTrigger()
    {
        _vfxCompo.StopVfx("FinalAttack");
    }
    
    private void HandleDamageCasterTrigger()
    {
        print(damageCaster);
        damageCaster.CastDamage(_player.transform.position,Vector3.forward,attackDataList[ComboCounter]);
    }

    public void Attack()
    {
        var comboCounterOver = ComboCounter > 2;
        var comboWindowExhaust = Time.time > _lastAttackTime + comboWindow;
        if (comboCounterOver || comboWindowExhaust) ComboCounter = 0;
        _entityAnimator.SetParam(_comboCounterHash, ComboCounter);
    }

    private void StartCharge()
    {
        if (_chargeRoutine == null)
            _chargeRoutine = StartCoroutine(HoldAttackCoroutine());
    }

    private void StopCharge()
    {
        if (_chargeRoutine != null)
        {
            StopCoroutine(_chargeRoutine);
            _chargeRoutine = null;
        }
    }

    private void HandleSwing()
    {
        _player._movement.StopImmediately();
        _player._movement.CanMove = false;
    }


    public void EndAttack()
    {
        ComboCounter++;
        IsAttack = false;
        _player._isSkilling = false;
        _lastAttackTime = Time.time;
    }

    private void AttackCancel()
    {
        IsAttack = true;
    }

    private void AttackMove()
    {
    }
    
    
    public AttackDataSO GetCurrentAttackData()
    {
        Debug.Assert(attackDataList.Length > ComboCounter, "Combo counter is out of range");
        return attackDataList[ComboCounter];
    }

    private IEnumerator HoldAttackCoroutine()
    {
        _player.isUsePowerAttack = false;
        _player.ChangeState("CHARGE");
        attackHoldTime = 0f;

        while (true)
        {
            attackHoldTime += Time.deltaTime;
            if (attackHoldTime >= MaxHoldTime) _player.isUsePowerAttack = true;
            yield return null;
        }
    }
    
}