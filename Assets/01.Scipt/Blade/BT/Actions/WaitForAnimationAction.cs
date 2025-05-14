using Blade.Entities;
using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "WaitForAnimation", story: "Wait for end [Trigger]", category: "Enemy/Animation", id: "5d24179cb10cbc818feaa3c938c3b89a")]
public partial class WaitForAnimationAction : Action
{
    [SerializeReference] public BlackboardVariable<EntityAnimatorTrigger> Trigger;


    private bool _isTrriggered;
    protected override Status OnStart()
    {
        _isTrriggered = false;
        Trigger.Value.OnAnimationEndTrigger += HandleAnimationEnd;
        return Status.Running;
    }

    private void HandleAnimationEnd() => _isTrriggered = true;

    protected override Status OnUpdate()
    {
        if ((_isTrriggered))
            return Status.Success;

        return Status.Running;
    }

    protected override void OnEnd()
    {
        Trigger.Value.OnAnimationEndTrigger -= HandleAnimationEnd;
    }
}

