using System;
using System.Linq;
using Blade.Combat;
using Blade.Entities;
using UnityEngine;

namespace _01.Scipt.Player.Skill
{
    public class UpSkill : SkillCompo
    {
        private float updamage;

        private Player.Player _player;
        
        private ActionData _actionData;

        [SerializeField] private UpSkillDamageCaster damageCaster;
        public override void GetSkill()
        {
            _player = _entity as Player.Player;
            updamage = _stat.GetStat(_skillDamage).Value;
            _player.PlayerInput.OnHighAttackPresssed += HandleHighAttack;
            _triggerCompo.OnHighAttack += HandleSkillAttack;
            _triggerCompo.OnHighAttack += Skill;
        }

        private void HandleHighAttack()
        {
            if (CanUseSkill("UpSkill") && !_player._isSkilling)
            {
                _player.ChangeState("UP");
                CurrentTimeClear("UpSkill");
                
                _player._attackCompo.IsAttack = true;
                _player._isSkilling = true;
            }
            else
                return;
        }

        private void HandleSkillAttack()
        {
            damageCaster.CastDamage(_player.transform.position,Vector3.forward, null);
        }


        public override void EventDefault()
        {
            _player.PlayerInput.OnHighAttackPresssed -= HandleHighAttack;
            _triggerCompo.OnHighAttack -= HandleSkillAttack;
            _triggerCompo.OnHighAttack -= Skill;
        }


        protected override void Skill()
        {
            Collider[] collider = Physics.OverlapBox(transform.position, _skillSize,
                Quaternion.identity, _whatIsEnemy);
            

            foreach (var item in collider)
            {
                if (item.TryGetComponent(out IDamageable damage))
                {
                    damage.ApplyDamage(_skillDamage.Value,item.transform.position,null,null);
                    item.GetComponentInChildren<Rigidbody>().AddForce(Vector3.up * 7, ForceMode.Impulse);
                    Debug.Log("공격됨");
                }
                else
                {
                    print("왔는데 없음");
                }
            }
            
        }

        public override void SkillFeedback()
        {
            base.SkillFeedback();
        }

    }
}