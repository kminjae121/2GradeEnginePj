using Blade.Combat;
using Blade.Enemies;
using Blade.Entities;
using UnityEngine;

namespace _01.Scipt.Player.Skill
{
    public class PowerUpSkill : SkillCompo
    {
        private EntitySkillCompo skillCompo;
         
        private Player.Player _player;
        
        private ActionData _actionData;

        public override void GetSkill()
        {
            _player = _entity as Player.Player;
            skillCompo = GetComponent<EntitySkillCompo>();
            _player.PlayerInput.OnStrongAttackPressed += HandleHighAttack;
            _triggerCompo.PowerAttackTrigger += Skill;

        }

        private void HandleHighAttack()
        {
            if (CanUseSkill("PowerSkill") && !_player._isSkilling)
            {
                _player.ChangeState("POWER");
                _player._attackCompo.IsAttack = true;
                _player._isSkilling = true;
                CurrentTimeClear("PowerSkill");
                
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
                    damage.ApplyDamage(skillCompo.skillDamage,item.transform.position,null,null);
                    CameraShakingManager.instance.ShakeCam(0.1f,0.3f,5,20);
                    //item.GetComponentInChildren<Rigidbody>().AddForce(Vector3.up * 2.3f, ForceMode.Impulse);
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