namespace _01.Scipt.Player.State.Player
{
    public class PlayerUpAttackState : PlayerCanAttackState
    {
        public PlayerUpAttackState(Member.Kmj._01.Scipt.Entity.AttackCompo.Entity entity, int animationHash) : base(entity, animationHash)
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
            if(_isTriggerCall)
                _player.ChangeState("IDLE");
            base.Update();
        }

        public override void Exit()
        {
            _player._isSkilling = false;

            _player._attackCompo.IsAttack = false;
            _player._movement.CanMove = true;
            base.Exit();
        }
    }
}