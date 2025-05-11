using Member.Kmj._01.Scipt.Entity.AttackCompo;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class PlayerIdleState : PlayerCanAttackState
{
    private CharacterMovement _movement;
    public PlayerIdleState(Entity entity, int animationHash) : base(entity, animationHash)
    {
        _movement = entity.GetCompo<CharacterMovement>();
    }


    public override void Update()
    {
        base.Update();
        Vector2 movementKey = _player.PlayerInput.MovementKey;
        _movement.SetMove(movementKey.x ,movementKey.y);
        if (movementKey.magnitude > _inputThereshold && _player._movement.CanMove)
        {
            _player.ChangeState("MOVE");
        }
    }
}
