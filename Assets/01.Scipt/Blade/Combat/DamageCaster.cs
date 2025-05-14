using Blade.Entities;
using Member.Kmj._01.Scipt.Entity.AttackCompo;
using UnityEngine;

namespace Blade.Combat
{
    public abstract class DamageCaster : MonoBehaviour
    {
        [SerializeField] protected LayerMask whatIsEnemy;

        protected Entity _owner;

        public virtual void InitCster(Entity owenr)
        {
            _owner = owenr; 
        }

        public abstract void CastDamage(Vector3 position, Vector3 direction, AttackDataSO attackData);
    }
}