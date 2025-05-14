using Blade.Enemies;
using Blade.Entities;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "RotateFromAnimator", story: "[Movement] rotate to [Target] with [Trigger]", category: "Enemy/Move", id: "677b4f5a039effbe748977bfded41b31")]
public partial class RotateFromAnimatorAction : Action
{
    [SerializeReference] public BlackboardVariable<NavMovement> Movement;
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<EntityAnimatorTrigger> Trigger;

    private bool _isRotate = false;
    protected override Status OnStart()
    {
        Trigger.Value.OnManualRotationTrigger += HandleManualRotation;
        return Status.Running;
    }

    private void HandleManualRotation(bool isRotate) => _isRotate = isRotate;
    protected override Status OnUpdate()
    {
        if(_isRotate)
        {
            Movement.Value.LoookAtTarget(Target.Value.position);
        }
        return Status.Success;
    }

    protected override void OnEnd()
    {
        Trigger.Value.OnManualRotationTrigger -= HandleManualRotation;
    }
}


