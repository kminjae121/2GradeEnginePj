using System.Linq;
using UnityEngine;

namespace _01.Scipt.Player.Skill
{
    public class UpSkill : SkillCompo
    {
        private float updamage;

        private Player.Player _player;
        
        public override void GetSkill()
        {
            _player = _entity as Player.Player;
            updamage = _stat.GetStat(_skillDamage).Value;
            _player.PlayerInput.OnHighAttackPresssed += HandleHighAttack;
            _triggerCompo.OnHighAttack += Skill;
        }

        private void HandleHighAttack()
        {
            if (CanUseSkill("UpSkill") && !_player._isSkilling)
            {
                _player.ChangeState("UP");
                CurrentTimeClear("UpSkill");
                _player._isSkilling = true;
            }
            else
                return;
        }


        public override void EventDefault()
        {
            _player.PlayerInput.OnHighAttackPresssed -= HandleHighAttack;
            _triggerCompo.OnHighAttack -= Skill;
        }


        protected override void Skill()
        {
            Collider[] collider = Physics.OverlapBox(transform.position, _skillSize,
                Quaternion.identity, _whatIsEnemy);
            

            foreach (var item in collider)
            {
                item.GetComponentInChildren<IDamgable>().ApplyDamage(updamage, false, 0, _player);
                
                item.GetComponentInChildren<Rigidbody>().AddForce(Vector3.up * 4, ForceMode.Impulse);
            }
            
        }

        public override void SkillFeedback()
        {
            base.SkillFeedback();
        }
    }
}