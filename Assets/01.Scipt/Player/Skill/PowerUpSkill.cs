using System;
using System.Collections.Generic;
using Blade.Combat;
using Blade.Enemies;
using Blade.Enemies.Skeletons;
using Blade.Entities;
using UnityEngine;

namespace _01.Scipt.Player.Skill
{
    public class PowerUpSkill : SkillCompo
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
            _player.PlayerInput.OnStrongAttackPressed += HandleHighAttack;
            _triggerCompo.PowerAttackTrigger += Skill;
            _triggerCompo.OnPowerAttackVFXTrigger += HandlePowerAttackTrigger;

        }

        
        private void HandlePowerAttackTrigger()
        {  
            if (skillEffectName[currentSkillEffectNameIdx] == String.Empty)
                return;
            else
                _vfxCompo.PlayVfx(skillEffectName[currentSkillEffectNameIdx], _entity.transform.position, Quaternion.identity);
        }
        
        private void HandleHighAttack()
        {
            if (CanUseSkill("PowerSkill") && !_player._isSkilling)
            {
                _player._movement.CanMove = false;
                _player._attackCompo.IsAttack = true;
                _player._isSkilling = true;
                _player.ChangeState("POWER");
                CurrentTimeClear("PowerSkill");
                
            }
            else
                return;
        }
        
        
        public override void EventDefault()
        {
            _player.PlayerInput.OnHighAttackPresssed -= HandleHighAttack;
            _triggerCompo.PowerAttackTrigger -= Skill;
            _triggerCompo.OnPowerAttackVFXTrigger -= HandlePowerAttackTrigger;
        }


        protected override void Skill()
        {
            AudioManager.Instance.PlaySFX("PoHo",0.3f);
            Collider[] collider = Physics.OverlapBox(transform.position, _skillSize,
                Quaternion.identity, _whatIsEnemy);
            

            foreach (var item in collider)
            {
                if (item.TryGetComponent(out IDamageable damage))
                {
                    PlayerComboSystem.Instance.RaiseCombo(3);
                    damage.ApplyDamage(skillCompo.skillDamage / 3,item.transform.position,null,null);
                    CameraShakingManager.instance.ShakeCam(0.1f,0.3f,5,20);
                    if (skillLevel == 1)
                    {
                        PlayerFuryManager.Instance.RaiseFury(4);
                    }
                    else if (skillLevel == 2)
                    {
                        PlayerFuryManager.Instance.RaiseFury(4);
                        item.GetComponent<EnemySkeletonSlave>().ChangeJumpChannelEvent();
                    }
                    else if (skillLevel == 3)
                    {
                        PlayerFuryManager.Instance.RaiseFury(4);
                        item.GetComponent<EnemySkeletonSlave>().HandleJumpAndStun();
                    }
                }
            }
        }

        public override void SkillFeedback()
        {
            base.SkillFeedback();
        }
        
    }
}