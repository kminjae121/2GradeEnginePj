using Blade.Entities;
using Member.Kmj._01.Scipt.Entity.AttackCompo;
using UnityEngine;

namespace Blade.Combat
{
    public interface IDamageable
    {
        public void ApplyDamage(float damage, Vector3 hitPoint, AttackDataSO _atkData, Entity dealer);
    }
}