
using UnityEngine;

namespace Member.Kmj._01.Scipt.Player.State.Player
{
    public class PlayerChargeState : PlayerCanAttackState
    {
        public PlayerChargeState(Entity.AttackCompo.Entity entity, int animationHash) : base(entity, animationHash)
        {
            
        }

        public override void Enter()
        {
            base.Enter();
            _player._movement.StopImmediately();
            _player._movement.CanMove = false;
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Exit()
        {
            _player._movement.CanMove = true;
            base.Exit();
        }
    }
}