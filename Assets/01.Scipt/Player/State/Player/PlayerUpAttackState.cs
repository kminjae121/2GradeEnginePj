namespace _01.Scipt.Player.State.Player
{
    public class PlayerUpAttackState : PlayerState
    {
        public PlayerUpAttackState(Member.Kmj._01.Scipt.Entity.AttackCompo.Entity entity, int animationHash) : base(entity, animationHash)
        {
            
        }

        public override void Enter()
        {
            base.Enter();
            _player._movement.StopImmediately();
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
            base.Exit();
        }
    }
}