    using System;
using System.Collections.Generic;
using System.Linq;
using Blade.Combat;
using Blade.Enemies;
using Blade.Entities;
using UnityEngine;

namespace _01.Scipt.Player.Skill
{
    public class UpSkill : SkillCompo
    {
        private EntitySkillCompo skillCompo;
        private Player.Player _player;
        
        
        private ActionData _actionData;
        
        private EntityVFX _vfxCompo;
        

        public override void GetSkill()
        {
            _player = _entity as Player.Player;
            skillCompo = GetComponent<EntitySkillCompo>();
            _vfxCompo = _entity.GetCompo<EntityVFX>();
            _player.PlayerInput.OnHighAttackPresssed += HandleHighAttack;
            
            _triggerCompo.OnPowerAttackVFXTrigger += HandleUpSkillTrigger;
            _triggerCompo.OnHighAttack += Skill;
        }
        
        private void HandleUpSkillTrigger()
        {
            if (skillEffectName[currentSkillEffectNameIdx] == null)
                return;
            else
                _vfxCompo.PlayVfx(skillEffectName[currentSkillEffectNameIdx], _entity.transform.position, Quaternion.identity);
        }

        private void HandleHighAttack()
        {
            if (CanUseSkill("UpSkill") && !_player._isSkilling)
            {
                _player.ChangeState("UP");
                _player._attackCompo.IsAttack = true;
                _player._isSkilling = true;
                CurrentTimeClear("UpSkill");
                
            }
            else
                return;
        }
        


        public override void EventDefault()
        {
            _triggerCompo.OnPowerAttackVFXTrigger -= HandleUpSkillTrigger;
            _player.PlayerInput.OnHighAttackPresssed -= HandleHighAttack;
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
                    damage.ApplyDamage(skillCompo.skillDamage,item.transform.position,null,null);
                    CameraShakingManager.instance.ShakeCam(0.1f,0.7f,5,40);
                    //item.GetComponentInChildren<Rigidbody>().AddForce(Vector3.up * 7, ForceMode.Impulse);
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