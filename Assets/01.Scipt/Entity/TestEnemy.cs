using Blade.Enemies;
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

    }

    protected override void HandleHit()
    {

    }

    protected override void HandleStun()
    {
    }
}
