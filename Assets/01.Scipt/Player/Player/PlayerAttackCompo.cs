using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using _01.Scipt.Blade.Combat;
using _01.Scipt.Player.Player;
using a;
using Blade.Combat;
using Blade.Core.StatSystem;
using Member.Kmj._01.Scipt.Entity.AttackCompo;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class PlayerAttackCompo : MonoBehaviour, IEntityComponet, IAfterInit
{
    public AttackDataSO[] attackDataList;

    [SerializeField] private float comboWindow;

    [SerializeField] private LayerMask _whatIsEnemy;

    

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

    [Space(10f)]
    [Header("AttackStat")]
    [SerializeField] private StatSO _atkDamageSO;

    [SerializeField] private StatSO _bloodHpSO;
    private EntityStat _statCompo;
    public float atkDamage { get; set; }
    public float bloodHp { get; private set; }
    private Lifesteal _StealCompo;
        
    [SerializeField] private List<GameObject> _attackSlash;
    [SerializeField] private Transform _bladeTransform;
    public int slashPercent { get; set; } = 20;
    
    public float baseAtkDamage { get; private set; }
    public float AttackSpeed
    {
        get => _attackSpeed;
        set
        {
            _attackSpeed = value;
            _entityAnimator.SetParam(_attackSpeedHash, _attackSpeed);
        }
    }


    public void Initialize(Entity entity)
    {
        _entity = entity;
        _player = entity as Player;
        _entityAnimator = entity.GetCompo<EntityAnimator>();
        _statCompo = entity.GetCompo<EntityStat>();
        
        _vfxCompo = entity.GetCompo<EntityVFX>();
        AttackSpeed = 1.6f;
        _StealCompo = GetComponentInChildren<Lifesteal>();
        //damageCast.InitCaster(_entity);
        _triggerCompo = entity.GetCompo<EntityAnimatorTrigger>();
        _triggerCompo.OnAttackAnimEnd += EndAttack;
        _triggerCompo.OnAttackTriggerEnd += HandleDamageCasterTrigger;
        _triggerCompo.OnAttackCancel += AttackCancel;
        _triggerCompo.OnAttackVFXTrigger += HandleAttackVFXTrigger;
        _triggerCompo.OnAttackFinalVFXTrigger += HandleFinalAttackTrigger;
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
        StatSO targetStat = _statCompo.GetStat(_atkDamageSO);
        Debug.Assert(targetStat != null, $"{_atkDamageSO.statName} stat could not be found");
        targetStat.OnValudeChanged -= HandleAttackStatChange;
        
        
        StatSO targetStat2 = _statCompo.GetStat(_bloodHpSO);
        Debug.Assert(targetStat2 != null, $"{_bloodHpSO.statName} stat could not be found");
        targetStat2.OnValudeChanged -= HandleAttackStatChange;
    }
    
   
    private void HandleAttackStatChange(StatSO stat, float currentValue, float previousValue)
    {
        atkDamage += currentValue - previousValue;
        baseAtkDamage += currentValue - previousValue;

        BaseStatLibrary.instance.baseTxt.GetValueOrDefault("AtkDamage").text = $"공격력 : {baseAtkDamage}";
    }
    
    private void HandleBloodStatChange(StatSO stat, float currentValue, float previousValue)
    {
        bloodHp += currentValue;
        BaseStatLibrary.instance.baseTxt.GetValueOrDefault("BloodEat").text = $"흡혈 : {bloodHp}";
    }

   
    public void AfterInit()
    {
        StatSO targetStat = _statCompo.GetStat(_atkDamageSO);
        Debug.Assert(targetStat != null, $"{_atkDamageSO.statName} stat could not be found");
        targetStat.OnValudeChanged += HandleAttackStatChange;
        atkDamage = targetStat.BaseValue;
        baseAtkDamage = targetStat.BaseValue;
        
        StatSO targetStat2 = _statCompo.GetStat(_bloodHpSO);
        Debug.Assert(targetStat2 != null, $"{_bloodHpSO.statName} stat could not be found");
        targetStat2.OnValudeChanged += HandleBloodStatChange;
        bloodHp = targetStat2.BaseValue;
    }

    private void Start()
    {
        BaseStatLibrary.instance.baseTxt.GetValueOrDefault("AtkDamage").text = $"공격력 : {baseAtkDamage}";
        BaseStatLibrary.instance.baseTxt.GetValueOrDefault("BloodEat").text = $"흡혈 : {bloodHp}";

        BaseStatLibrary.instance.baseTxt.GetValueOrDefault("slashProbability").text = $"검기확률 : {slashPercent}%";
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
        IntilalizeAttackSlash();
        damageCaster.CastDamage(_player.transform.position,Vector3.forward,attackDataList[ComboCounter]);
    }
    
    public void Attack()
    {
        var comboCounterOver = ComboCounter > 2;
        var comboWindowExhaust = Time.time > _lastAttackTime + comboWindow;
        if (comboCounterOver || comboWindowExhaust) ComboCounter = 0;
        _entityAnimator.SetParam(_comboCounterHash, ComboCounter);
    }

    private void IntilalizeAttackSlash()
    {
        int rand = Random.Range(0, 101);

        if (PlayerFuryManager.Instance.isInRange)
        {
            Quaternion rot = Quaternion.Euler(0f, _bladeTransform.rotation.eulerAngles.y, 0f);
            GameObject slash = Instantiate(_attackSlash[ComboCounter], _bladeTransform.position, rot);
        
            SlashCompo slashCompo = slash.GetComponent<SlashCompo>();
            if (slashCompo != null)
            {
                slashCompo.TargetRotationSource = _bladeTransform;
            }
        }
        else if (rand <= slashPercent)
        {
            Quaternion rot = Quaternion.Euler(0f, _bladeTransform.rotation.eulerAngles.y, 0f);
            GameObject slash = Instantiate(_attackSlash[ComboCounter], _bladeTransform.position, rot);
        
            SlashCompo slashCompo = slash.GetComponent<SlashCompo>();
            if (slashCompo != null)
            {
                slashCompo.TargetRotationSource = _bladeTransform;
            }
        }
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
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, _boxsize);
        Gizmos.color = Color.white;
    }
    
}
