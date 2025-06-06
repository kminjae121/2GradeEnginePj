using Blade.Enemies;
using System;
using Blade.Enemies.Skeletons;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SelfIsChangeState", story: "[Self] is [ChangeState]", category: "Action", id: "a7e8898f430da76c4ae55f60c7320bc1")]
public partial class SelfIsChangeStateAction : Action
{
    [SerializeReference] public BlackboardVariable<Enemy> Self;
    [SerializeReference] public BlackboardVariable<EnemyState> ChangeState;

    protected override Status OnStart()
    {
        EnemySkeletonSlave enemy = Self.Value as EnemySkeletonSlave;

        enemy._State = ChangeState;
        return Status.Success;
    }

}

