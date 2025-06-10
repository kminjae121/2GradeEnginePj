    using System;
using System.Collections.Generic;
using System.Linq;
using Blade.Combat;
using Blade.Enemies;
using Blade.Enemies.Skeletons;
using Blade.Entities;
using UnityEngine;

namespace _01.Scipt.Player.Skill
{
    public class UpSkill : SkillCompo
    {
        private EntitySkillCompo skillCompo;
        private Player.Player _player;
        
        
        private ActionData _actionData;
        
        [field: SerializeField] public List<GameObject> _slashEffectt { get; set; }
        private EntityVFX _vfxCompo;
        public List<Transform> SlashTrans;

        public override void GetSkill()
        {
            _player = _entity as Player.Player;
            skillCompo = GetComponent<EntitySkillCompo>();
            _vfxCompo = _entity.GetCompo<EntityVFX>();
            _player.PlayerInput.OnHighAttackPresssed += HandleHighAttack;
            _triggerCompo.OnHighAttackVFXTrigger += HandleUpSkillTrigger;
            _triggerCompo.OnHighAttack += Skill;
        }
        
        private void HandleUpSkillTrigger()
        {
            if (skillEffectName[currentSkillEffectNameIdx] == String.Empty)
                return;
            AudioManager.Instance.PlaySFX("UpSkill");
            if (skillLevel == 2)
            {
                Quaternion rot = Quaternion.Euler(0f, SlashTrans[0].rotation.eulerAngles.y, 0f);
                GameObject slash = Instantiate(_slashEffectt[currentSkillEffectNameIdx], SlashTrans[0].position, rot);
        
                SlashCompo slashCompo = slash.GetComponent<SlashCompo>();
                if (slashCompo != null)
                {
                    slashCompo.TargetRotationSource = SlashTrans[0]; // 정보용으로 유지
                    CameraShakingManager.instance.ShakeCam(0.1f, 0.3f, 5, 20);
                }
            }
            else if(skillLevel == 3)
            {
                AudioManager.Instance.PlaySFX("UpSkill");
                foreach (Transform ts in SlashTrans)
                {
                    Quaternion rot = Quaternion.Euler(0f, ts.rotation.eulerAngles.y, 0f);
                    GameObject slash = Instantiate(_slashEffectt[currentSkillEffectNameIdx], ts.position, rot);
            
                    SlashCompo slashCompo = slash.GetComponent<SlashCompo>();
                    if (slashCompo != null)
                    {
                        slashCompo.TargetRotationSource = ts; // 정보용으로 유지
                        CameraShakingManager.instance.ShakeCam(0.1f, 0.3f, 5, 20);
                    }
                }
            }
        }

        private void HandleHighAttack()
        {
            if (CanUseSkill("UpSkill") && !_player._isSkilling)
            { 
                _player._movement.CanMove = false;
                _player._attackCompo.IsAttack = true;
                _player._isSkilling = true;
                _player.ChangeState("UP");
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
                    PlayerComboSystem.Instance.RaiseCombo(3);
                    PlayerFuryManager.Instance.RaiseFury(12);
                    CameraShakingManager.instance.ShakeCam(0.1f,0.1f,5,40);
                    item.GetComponent<EnemySkeletonSlave>().ChangeJumpChannelEvent();
                }
            }
            
        }

        public override void SkillFeedback()
        {
            base.SkillFeedback();
        }

    }
}