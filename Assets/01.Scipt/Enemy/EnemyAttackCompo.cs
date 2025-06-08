using System;
using Blade.Combat;
using Blade.Enemies.Skeletons;
using UnityEngine;

namespace _01.Scipt.Enemy
{
    public class EnemyAttackCompo : DamageCaster
    {
        [SerializeField] private Vector3 boxSize;
        private EntityAnimatorTrigger _triggerCompo;

        private void Awake()
        {
            _triggerCompo = transform.parent.GetComponentInChildren<EntityAnimatorTrigger>();
        }

        private void Start()
        {
            _triggerCompo.OnAttackTrigger += Attack;
        }

        public void Attack()
        {
            CastDamage(transform.position, Vector3.zero, null);
        }

        public override void CastDamage(Vector3 position, Vector3 direction, AttackDataSO attackData)
        {
            var collider = Physics.OverlapBox(transform.position, boxSize,
                Quaternion.identity,whatIsEnemy);
            
            if(collider == null)
                return;
            
            foreach (var Obj in collider)
            {
                print(Obj.name);
                Obj.GetComponentInChildren<EntityHealth>().ApplyDamage(20,Obj.transform.position,attackData,null);
                CameraShakingManager.instance.ShakeCam(0.1f,0.6f,5,20);
            }
        }

        private void OnDisable()
        {
            
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