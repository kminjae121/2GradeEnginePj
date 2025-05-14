using Blade.Entities;
using Member.Kmj._01.Scipt.Entity.AttackCompo;
using UnityEngine;

namespace Blade.Combat
{
    public interface IDamageable
    {
        public void ApplyDamage(float damage, Vector3 hitPoint, Vector3 hitNormal, AttackDataSO _atkData, Entity dealer);
    }
}