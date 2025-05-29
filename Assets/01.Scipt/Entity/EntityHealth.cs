using System;
using Blade.Core.StatSystem;
using Blade.Entities;
using Member.Kmj._01.Scipt.Entity.AttackCompo;
using UnityEngine;

namespace Blade.Combat
{
    public class EntityHealth : MonoBehaviour, IEntityComponet, IDamageable, IAfterInit
    {
        private Entity _entity;
        private ActionData _actionData;
        private EntityStat _statCompo;

        
        [SerializeField] private StatSO hpStat;
        [field:SerializeField] public float maxHealth {get; set;}
        [field:SerializeField] public float currentHealth  {get; set;}
        
        public void Initialize(Entity entity)
        {
            _entity = entity;
            _actionData = entity.GetCompo<ActionData>();
            _statCompo = entity.GetCompo<EntityStat>();
        }
        public void AfterInit()
        {
            StatSO target = _statCompo.GetStat(hpStat);
            Debug.Assert(target != null, $"{hpStat.statName} does not exist");
            target.OnValudeChanged += HandleMaxHPChanged;
            currentHealth = maxHealth = target.Value;
        }
        

        private void OnDestroy()
        {
            StatSO target = _statCompo.GetStat(hpStat);
            Debug.Assert(target != null, $"{hpStat.statName} does not exist");
            target.OnValudeChanged -= HandleMaxHPChanged;
        }

        private void HandleMaxHPChanged(StatSO stat, float currentvalue, float previousvalue)
        {
            //float changed = currentvalue - previousvalue;
            currentHealth += currentvalue;
            //currentHealth = Mathf.Clamp(currentvalue + changed, 0, maxHealth);
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
}
  
