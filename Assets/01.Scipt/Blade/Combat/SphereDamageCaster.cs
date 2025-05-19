using System;
using Blade.Combat;
using UnityEngine;

namespace a
{
    public class SphereDamageCaster : DamageCaster
    {
        [SerializeField, Range(0.5f, 3f)] private float castRadius = 1f;

        [SerializeField, Range(0, 1f)] private float castInterpolation = 0.5f;
        [SerializeField, Range(0, 3f)] private float castingRange = 1f;
        
        public override void CastDamage(Vector3 position, Vector3 direction, Blade.Combat.AttackDataSO attackData)
        {
            Vector3 startPos = position + direction * castInterpolation;

            bool isHit = Physics.SphereCast(
                startPos, castRadius,
                transform.forward,
                out RaycastHit hit,
                castingRange,
                whatIsEnemy);

            if (isHit)
            {
                if (hit.collider.TryGetComponent(out IDamageable damageable))
                {
                    float damage = 5f;
                    
                }
            }
            else
            {
               
            }
        }
        

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Vector3 startPos = transform.position + transform.forward * castInterpolation * 2;

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(startPos, castRadius);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(startPos + transform.forward * castInterpolation, castRadius);
        }
        
        #endif
    }
}