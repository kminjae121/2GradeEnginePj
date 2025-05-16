using Blade.Entities;
using Member.Kmj._01.Scipt.Entity.AttackCompo;
using UnityEngine;
using UnityEngine.Rendering;

namespace Blade.Combat
{
    public abstract class DamageCaster : MonoBehaviour
    {
        [SerializeField] protected LayerMask whatIsEnemy;

        //[SerializeField] protected float atkDamage;
        [SerializeField] protected EntityStat _Stat;
        [SerializeField] protected StatSO _atkdamage;
        protected Entity _owner;

        public virtual void InitCster(Entity owenr)
        {
            _owner = owenr; 
        }

        public abstract void CastDamage(Vector3 position, Vector3 direction, AttackDataSO attackData);
    }
}