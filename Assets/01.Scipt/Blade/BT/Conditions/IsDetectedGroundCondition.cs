using Blade.Enemies;
using System;
using Unity.Behavior;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "IsDetectedGroundCondition", story: "[Self] is on the Ground with [Movement]", category: "Conditions", id: "f74faaa476a01e3dcb71fba1cf569257")]
public partial class IsDetectedGroundCondition : Condition
{
    [SerializeReference] public BlackboardVariable<Enemy> Self;
    [SerializeReference] public BlackboardVariable<NavMovement> Movement;
    

    public override bool IsTrue()
    {
        if (Movement.Value == null || Self.Value == null)
            return false;

        bool isOnGround = Movement.Value.IsGrounded();

        return Movement.Value.IsGrounded() == true;
    }
    
}
