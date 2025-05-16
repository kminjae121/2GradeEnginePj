using System;
using Blade.Combat;
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

    private void OnDestroy()
    {
        _statCompo.GetStat(hpStat).OnValueChange -= HandleHpChange;
        _entity.OnDamage -= ApplyDamage;
    }

    private void Start()
    {
        AfterInit();
    }


    public void Initialize(Entity entity)
    {
        _entity = entity;
        _statCompo = entity.GetCompo<EntityStat>();
    }

    public void AfterInit()
    {
        _statCompo.GetStat(hpStat).OnValueChange += HandleHpChange;
        currentHealth = maxHealth = _statCompo.GetStat(hpStat).Value;
        _entity.OnDamage += ApplyDamage;
    }

    private void HandleHpChange(StatSO stat, float current, float previous)
    {
        maxHealth = current;
        Debug.Log(currentHealth);
        currentHealth = Mathf.Clamp(currentHealth + current - previous, 1f, maxHealth);
    }
    
    

    public void ApplyDamage(float damage, AttackDataSO _atkData, Entity dealer)
    {
        if (_entity.IsDead) return;

        currentHealth = Mathf.Clamp(currentHealth -= damage, 0, maxHealth);

        //_feedbackData.IsLastStopHit = isHit;
        //_feedbackData.LastEntityWhoHit = delear;
        //_feedbackData.LastStunLevel = StunLevel;

        _entity.OnHit?.Invoke();    
        if (currentHealth <= 0)
        {
            Debug.Log("�ֱ�");
            _entity.OnDead?.Invoke();
        }
    }
}
