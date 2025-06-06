using System;
using Blade.Combat;
using Blade.Enemies.Skeletons;
using Blade.Entities;
using UnityEngine;

namespace _01.Scipt.Blade.Combat
{
    public class OverlapDamageCaster : DamageCaster
    {
        [SerializeField] private Vector3 boxSize;

        private void Awake()
        {
            _steal = GetComponent<Lifesteal>();
        }

        public override void CastDamage(Vector3 position, Vector3 direction, AttackDataSO attackData)
        {
            var collider = Physics.OverlapBox(transform.position, boxSize,
                Quaternion.identity,whatIsEnemy);


            foreach (var Obj in collider)
                if (Obj.TryGetComponent(out IDamageable damage))
                {
                    _steal.UpGradeStat();
                    damage.ApplyDamage(attackCompo.atkDamage,Obj.transform.position,attackData,null);
                    PlayerFuryManager.Instance.RaiseFury(4f);
                    CameraShakingManager.instance.ShakeCam(0.1f,0.2f,5,10);
                    Obj.GetComponentInChildren<EnemySkeletonSlave>().hitCoun += 1;
                    if (PlayerFuryManager.Instance.isInRange == true)
                    {
                        Obj.GetComponent<EnemySkeletonSlave>().HandleHaveToStun();
                    }
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
