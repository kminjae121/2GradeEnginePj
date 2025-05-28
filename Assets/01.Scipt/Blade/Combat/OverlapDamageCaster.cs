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
            atkDamage = _Stat.GetStat(_atkdamage).Value;
        }

        public override void CastDamage(Vector3 position, Vector3 direction, AttackDataSO attackData)
        {
            var collider = Physics.OverlapBox(transform.position, boxSize,
                Quaternion.identity,whatIsEnemy);


            foreach (var Obj in collider)
                if (Obj.TryGetComponent(out IDamageable damage))
                {
                    damage.ApplyDamage(atkDamage,Obj.transform.position,attackData,null);
                    CameraShakingManager.instance.ShakeCam(0.1f,0.6f,5,20);
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
