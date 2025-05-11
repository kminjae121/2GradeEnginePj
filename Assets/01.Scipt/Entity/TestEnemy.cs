using Member.Kmj._01.Scipt.Entity.AttackCompo;
using UnityEngine;

public class TestEnemy : Entity
{
    public EntityHealth _heath { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        _heath = GetCompo<EntityHealth>();
    }

    protected override void HandleDead()
    {
        throw new System.NotImplementedException();
    }

    protected override void HandleHit()
    {
        throw new System.NotImplementedException();
    }

    protected override void HandleStun()
    {
        throw new System.NotImplementedException();
    }
}
