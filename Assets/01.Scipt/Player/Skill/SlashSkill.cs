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
                _player.ChangeState("SLASH");
                _player._attackCompo.IsAttack = true;
                _player._isSkilling = true;
                CurrentTimeClear("SlashSkill");
                
            }
            else
                return;
        }

        public void MakeSlashEffect()
        {
            if (skillLevel < 3)
            {
                GameObject slash = Instantiate(_slashEffectt[currentEffectNum], SlashTrans[0].position, Quaternion.Euler(0, 0, 90));
                
                SlashCompo slashCompo = slash.GetComponent<SlashCompo>();
                if (slashCompo != null)
                {
                    slashCompo.TargetRotationSource = SlashTrans[0];
                }
            }
            else
            {
                foreach (Transform ts in SlashTrans)
                {
                    GameObject slash = Instantiate(_slashEffectt[currentEffectNum], ts.position, Quaternion.identity);
                    
                    SlashCompo slashCompo = slash.GetComponent<SlashCompo>();
                    if (slashCompo != null)
                    {
                        slashCompo.TargetRotationSource = ts;
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
