using System;
using Blade.Combat;
using Blade.Entities;
using Member.Kmj._01.Scipt.Entity;
using Member.Kmj._01.Scipt.Entity.AttackCompo;
using UnityEngine;

public class EntityHealth : MonoBehaviour, IDamageable, IEntityComponet,IAfterInit
{
    [SerializeField] private StatSO hpStat;
//
    public float maxHealth;

    [field : SerializeField] public float currentHealth { get; private set; }
    public event Action<Vector2> OnKnockback;

    private Entity _entity;
    [SerializeField] private EntityStat _statCompo;
   // private EntityFeedbackData _feedbackData;
   private ActionData _actionData;

    private void OnDestroy()
    {
        _statCompo.GetStat(hpStat).OnValueChange -= HandleHpChange;
    }

    private void Start()
    {
        AfterInit();
    }


    public void Initialize(Entity entity)
    {
        _entity = entity;
        _statCompo = entity.GetCompo<EntityStat>();
        _actionData = entity.GetCompo<ActionData>();
    }

    public void AfterInit()
    {
        _statCompo.GetStat(hpStat).OnValueChange += HandleHpChange;
        currentHealth = maxHealth = _statCompo.GetStat(hpStat).Value;
        
    }

    private void HandleHpChange(StatSO stat, float current, float previous)
    {
        maxHealth = current;
        Debug.Log(currentHealth);
        currentHealth = Mathf.Clamp(currentHealth + current - previous, 1f, maxHealth);
    }
    
    

    public void ApplyDamage(float damage, Vector3 hitPoint,AttackDataSO _atkData, Entity dealer)
    {
        print("공격됨");
        if (_entity.IsDead) return;

        currentHealth = Mathf.Clamp(currentHealth -= damage, 0, maxHealth);
    
        
        //_feedbackData.IsLastStopHit = isHit;
        //_feedbackData.LastEntityWhoHit = delear;
        //_feedbackData.LastStunLevel = StunLevel;
        _actionData.HitPoint = hitPoint;
        
        _entity.OnHit?.Invoke();    
        if (currentHealth <= 0)
        {
            _entity.OnDead?.Invoke();
        }
    }
}
