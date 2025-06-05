using System;
using Blade.Combat;
using Blade.Enemies.Skeletons;
using UnityEngine;

namespace _01.Scipt.Enemy
{
    public class EnemyLongRangeAttackCompo : DamageCaster
    {
        [SerializeField] private Vector3 boxSize;
        private EntityAnimatorTrigger _triggerCompo;
        [SerializeField] private GameObject _arrowPrefab;

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
            Instantiate(_arrowPrefab,position,Quaternion.LookRotation(transform.forward));
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