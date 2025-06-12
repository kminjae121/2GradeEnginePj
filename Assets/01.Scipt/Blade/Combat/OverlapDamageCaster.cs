 using System;
using Blade.Combat;
using Blade.Enemies;
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

            if (attackCompo.ComboCounter == 2)
            {
                AudioManager.Instance.PlaySFX("Slash2", 0.4f);
                AudioManager.Instance.PlaySFX($"Slash{attackCompo.ComboCounter}",0.3f);
            }
            else
                AudioManager.Instance.PlaySFX($"Slash{attackCompo.ComboCounter}",0.2f);

            foreach (var Obj in collider)
                if (Obj.TryGetComponent(out IDamageable damage))
                {
                    _steal.UpGradeStat();
                    damage.ApplyDamage(attackCompo.atkDamage,Obj.transform.position,attackData,null);
                    PlayerComboSystem.Instance.RaiseCombo(3);
                    PlayerFuryManager.Instance.RaiseFury(4f);
                    CameraShakingManager.instance.ShakeCam(0.1f,0.5f,5,10);
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
