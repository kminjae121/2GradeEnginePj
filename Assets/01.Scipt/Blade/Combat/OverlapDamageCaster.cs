using System;
using Blade.Combat;
using Blade.Entities;
using UnityEngine;

namespace _01.Scipt.Blade.Combat
{
    public class OverlapDamageCaster : DamageCaster
    {
        [SerializeField] private Vector3 boxSize;


        private void Awake()
        {
            
        }

        public override void CastDamage(Vector3 position, Vector3 direction, AttackDataSO attackData)
        {
            print("득옴");   
            var collider = Physics.OverlapBox(transform.position, boxSize,
                Quaternion.identity,whatIsEnemy);


            foreach (var Obj in collider)
                if (Obj.TryGetComponent(out IDamageable damage))
                {
                    damage.ApplyDamage(_atkdamage.Value,Obj.transform.position,attackData,null);
                    
                }
                else
                {
                    return;
                }
        }
        

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, boxSize);
            Gizmos.color = Color.white;
        }
        
#endif
    }
}
