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
                   // Obj.GetComponentInChildren<ActionData>().HitPoint = Obj.transform.position;
                    damage.ApplyDamage(_atkdamage.Value,Obj.transform.position,attackData,null);
                    Debug.Log("공격됨");
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
