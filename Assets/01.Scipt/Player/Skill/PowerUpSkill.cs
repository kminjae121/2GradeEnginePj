using Blade.Combat;
using Blade.Entities;
using UnityEngine;

namespace _01.Scipt.Player.Skill
{
    public class PowerUpSkill : SkillCompo
    {
         private float updamage;

        private Player.Player _player;
        
        private ActionData _actionData;
        
        public override void GetSkill()
        {
            _player = _entity as Player.Player;
            updamage = _stat.GetStat(_skillDamage).Value;
            _player.PlayerInput.OnStrongAttackPressed += HandleHighAttack;
            _triggerCompo.PowerAttackTrigger += Skill;
        }

        private void HandleHighAttack()
        {
            if (CanUseSkill("PowerSkill") && !_player._isSkilling)
            {
                _player.ChangeState("POWER");
                CurrentTimeClear("PowerSkill");
                
                _player._attackCompo.IsAttack = true;
                _player._isSkilling = true;
            }
            else
                return;
        }
        
        
        public override void EventDefault()
        {
            _player.PlayerInput.OnHighAttackPresssed -= HandleHighAttack;
            _triggerCompo.PowerAttackTrigger -= Skill;
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
                    
                    item.GetComponentInChildren<Rigidbody>().AddForce(Vector3.up * 2.3f, ForceMode.Impulse);
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