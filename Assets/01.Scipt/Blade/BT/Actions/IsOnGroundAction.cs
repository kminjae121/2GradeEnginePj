using Blade.Enemies;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.AI;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "isOnGround", story: "[Self] is On the Ground", category: "Action", id: "462efcbdbdbbb4947ce026d010a370d9")]
public partial class IsOnGroundAction : Action
{
    [SerializeReference] public BlackboardVariable<Enemy> Self;

    protected override Status OnStart()
    {
        var enemy = Self.Value;
        var agent = enemy.GetComponent<NavMeshAgent>();
        if (agent == null)
            return Status.Failure;

        agent.enabled = true;
        return Status.Running;
    }
}

