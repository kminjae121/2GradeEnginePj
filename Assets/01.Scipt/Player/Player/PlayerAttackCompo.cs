using System;
using System.Collections;
using _01.Scipt.Player.Player;
using Member.Kmj._01.Scipt.Entity.AttackCompo;
using UnityEngine;

public class PlayerAttackCompo : MonoBehaviour, IEntityComponet
{
    public AttackDataSO[] attackDataList;
    [SerializeField] private DamageCaster damageCast;

    [SerializeField] private float comboWindow;

    [SerializeField] private LayerMask _whatIsEnemy;

    [SerializeField] private StatSO _atkDamage;
    [SerializeField] private EntityStat _stat;

    [SerializeField] private Vector3 _boxsize;

    [field: SerializeField] public float MaxHoldTime { get; set; }
    [field: SerializeField] public bool IsAttack { get; set; }

    public bool useMouseDirection;

    [field: SerializeField] public Transform swingTrm;

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

    public float atkDamage { get; set; }

    public int ComboCounter { get; set; }


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

        atkDamage = _stat.GetStat(_atkDamage).Value;
        
        _vfxCompo = entity.GetCompo<EntityVFX>();
        AttackSpeed = 1.2f;
        //damageCast.InitCaster(_entity);
        _triggerCompo = entity.GetCompo<EntityAnimatorTrigger>();
        _triggerCompo.OnAttackTriggerEnd += HandleAttackTrigger;
        _triggerCompo.OnSwingAttackTrigger += HandleSwing;
        _triggerCompo.OnAttackAnimEnd += EndAttack;
        _triggerCompo.OnAttackCancel += AttackCancel;
        _triggerCompo.OnAttackVFXTrigger += HandleAttackVFXTrigger;
        _triggerCompo.OnAttackFinalVFXTrigger += HandleFinalAttackTrigger;
        _player.PlayerInput.OnChargeAttackPressed += StartCharge;
        _player.PlayerInput.OnChargeAttackCanceled += StopCharge;
    }


    private void OnDisable()
    {
        _triggerCompo.OnAttackVFXTrigger -= HandleAttackVFXTrigger; 
        _triggerCompo.OnAttackAnimEnd -= EndAttack;
        _triggerCompo.OnAttackCancel -= AttackCancel;
        
        _triggerCompo.OnAttackFinalVFXTrigger -= HandleFinalAttackTrigger;
        _player.PlayerInput.OnChargeAttackPressed -= StartCharge;
        _player.PlayerInput.OnChargeAttackCanceled -= StopCharge;
    }

    private void HandleAttackTrigger()
    {
       // var knockbackForce = new Vector2(6, 6);
        //   bool success = damageCast.CastDamage(atkDamage);

        var collider = Physics.OverlapBox(transform.position, _boxsize,
            Quaternion.identity, _whatIsEnemy);


        foreach (var Obj in collider)
            if (Obj.TryGetComponent(out IDamgable damage))
            {
                Debug.Log("공격됨");
                damage.ApplyDamage(10, true, 0, _player);
            }
            else
            {
                print("왔는데 없음");
            }
    }
    
    
    private void HandleAttackVFXTrigger() 
    {
        _vfxCompo.PlayVfx($"AttackVFX{ComboCounter}", Vector3.zero, Quaternion.identity);
    }
    private void HandleFinalAttackTrigger()
    {
        _vfxCompo.PlayVfx($"FinalAttack", Vector3.zero, Quaternion.identity);
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