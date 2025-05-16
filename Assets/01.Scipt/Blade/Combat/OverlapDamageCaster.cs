using System;
using Blade.Combat;
using UnityEngine;

namespace _01.Scipt.Blade.Combat
{
    public class OverlapDamageCaster : DamageCaster
    {
        [SerializeField] private Vector3 boxSize;
        

        public override void CastDamage(Vector3 position, Vector3 direction, AttackDataSO attackData)
        {
            Vector3 startPos = position;

            var collider = Physics.OverlapBox(transform.position, boxSize,
                Quaternion.identity,whatIsEnemy);


            foreach (var Obj in collider)
                if (Obj.TryGetComponent(out IDamageable damage))
                {
                    Debug.Log("공격됨");
                       damage.ApplyDamage(_atkdamage.Value,attackData,_owner);
                }
                else
                {
                    print("왔는데 없음");
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
