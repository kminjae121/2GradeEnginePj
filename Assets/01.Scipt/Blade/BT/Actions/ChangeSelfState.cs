using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "New Action", story: "[Self] is Change State", category: "Action", id: "5bdc0cddcbdd88079b47853f87c94739")]
public partial class ChangeSelfState : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;

    protected override Status OnStart()
    {
        
        return Status.Success;
    }
}

