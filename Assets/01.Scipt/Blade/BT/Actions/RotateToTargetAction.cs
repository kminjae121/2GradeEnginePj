using System;
using Blade.Enemies;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace Blade.BT.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "RotateToTarget", story: "[Movement] rotate to [Target] in [Second]", category: "Action", id: "5afdef7a248e1fc964de452a417d9330")]
    public partial class RotateToTargetAction : Action
    {
        [SerializeReference] public BlackboardVariable<NavMovement> Movement;
        [SerializeReference] public BlackboardVariable<Transform> Target;
        [SerializeReference] public BlackboardVariable<Transform> Self;

        private const float _rotationSpeed = 20f;
    
        protected override Status OnStart()
        {
            return Status.Running;
        }

        protected override Status OnUpdate()
        {
            LookTargetSmoothly();

            if(LookTargetSmoothly())
                return Status.Success;
            return Status.Running;
        }

        private bool LookTargetSmoothly()
        {
            Quaternion targetRot = Movement.Value.LoookAtTarget(Target.Value.position);

            float angleThreshold = 5f;

            return Quaternion.Angle(targetRot, Self.Value.rotation) < angleThreshold;
        }

    }
}

