using UnityEngine;

namespace Member.Kmj._01.Scipt.Player.State.Player
{
    public class PlayerSwingAttackState : PlayerState
    {
        public PlayerSwingAttackState(Entity.AttackCompo.Entity entity, int animationHash) : base(entity, animationHash)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            
            if (_isTriggerCall)
            {
                _player.ChangeState("IDLE");
            }
            
            base.Update();
        }

        public override void Exit()
        {
            _player._movement.CanMove = true;
            
            base.Exit();
            
        }
    }
}