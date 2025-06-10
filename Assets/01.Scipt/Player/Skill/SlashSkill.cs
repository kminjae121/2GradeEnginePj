using System.Collections.Generic;
using Blade.Entities;
using UnityEngine;

namespace _01.Scipt.Player.Skill
{
    public class SlashSkill :SkillCompo
    {
        private EntitySkillCompo skillCompo;
        private Player.Player _player;
        
        
        private ActionData _actionData;

        public List<Transform> SlashTrans;
        
        
        private EntityVFX _vfxCompo;

        [field: SerializeField] public List<GameObject> _slashEffectt { get; set; }

        public int currentEffectNum { get; set; } = 0;
        
        
        public override void GetSkill()
        {
            _player = _entity as Player.Player;
            skillCompo = GetComponent<EntitySkillCompo>();
            _vfxCompo = _entity.GetCompo<EntityVFX>();
            _player.PlayerInput.OnSlashPressed += HandleSlashSkill;
            _triggerCompo.SlashVFXTrigger += MakeSlashEffect;

        }

        private void HandleSlashSkill()
        {
            if (CanUseSkill("SlashSkill") && !_player._isSkilling)
            {
                print("실행됨");
                _player._movement.CanMove = false;
                _player._attackCompo.IsAttack = true;
                _player._isSkilling = true;
                _player.ChangeState("SLASH");
                CurrentTimeClear("SlashSkill");
                
            }
            else
                return;
        }

        public void MakeSlashEffect()
        {
            AudioManager.Instance.PlaySFX("Slash");
            if (skillLevel < 3)
            {
                Quaternion rot = Quaternion.Euler(0f, SlashTrans[0].rotation.eulerAngles.y, 0f);
                GameObject slash = Instantiate(_slashEffectt[currentEffectNum], SlashTrans[0].position, rot);
        
                SlashCompo slashCompo = slash.GetComponent<SlashCompo>();
                if (slashCompo != null)
                {
                    slashCompo.TargetRotationSource = SlashTrans[0]; // 정보용으로 유지
                    CameraShakingManager.instance.ShakeCam(0.1f, 0.3f, 5, 20);
                }
            }
            else
            {
                foreach (Transform ts in SlashTrans)
                {
                    Quaternion rot = Quaternion.Euler(0f, ts.rotation.eulerAngles.y, 0f);
                    GameObject slash = Instantiate(_slashEffectt[currentEffectNum], ts.position, rot);
            
                    SlashCompo slashCompo = slash.GetComponent<SlashCompo>();
                    if (slashCompo != null)
                    {
                        slashCompo.TargetRotationSource = ts; // 정보용으로 유지
                        CameraShakingManager.instance.ShakeCam(0.1f, 0.3f, 5, 20);
                    }
                }
            }
        }
        


        public override void EventDefault()
        {
            _player.PlayerInput.OnSlashPressed -= HandleSlashSkill;
            _triggerCompo.SlashVFXTrigger -= MakeSlashEffect;
        }


        public override void SkillFeedback()
        {
            base.SkillFeedback();
        }
    }
}
