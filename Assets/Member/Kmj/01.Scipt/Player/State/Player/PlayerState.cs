using Member.Kmj._01.Scipt.Entity.AttackCompo;
using UnityEngine;

public abstract class PlayerState : EntityState
{
    protected Player _player;
    protected readonly float _inputThereshold = 0.1f;
    protected PlayerState(Entity entity, int animationHash) : base(entity, animationHash)
    {
        _player = entity as Player;
       // Debug.Assert(_player != null, $"Player state using only in player");
    }
}
