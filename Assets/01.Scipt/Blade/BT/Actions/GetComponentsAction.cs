using System;
using System.Collections.Generic;
using Blade.Enemies;
using Blade.Entities;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace Blade.BT.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "GetComponents", story: "Get components from [self]", category: "Action", id: "b4f809444b8cfbedbe08ffe67e3b82be")]
    public partial class GetComponentsAction : Action
    {
        [SerializeReference] public BlackboardVariable<Enemy> Self;

        protected override Status OnStart()
        {
            Enemy enemy = Self.Value;
            List<BlackboardVariable> varList = enemy.BTEnemy.BlackboardReference.Blackboard.Variables;

            foreach (var variable in varList)
            {
                if (typeof(IEntityComponet).IsAssignableFrom(variable.Type) == false) continue;

                SetVariable(enemy, variable.Name, enemy.Getcompo(variable.Type));
                
            }
            return Status.Success;
        }

        private void SetVariable<T>(Enemy enemy, string variableName, T component)
        {
            Debug.Assert(component != null, $"Check {variableName} component not exist on {enemy.gameObject.name}");

            if(enemy.BTEnemy.GetVariable(variableName,out BlackboardVariable variable))
            {
                variable.ObjectValue = component;
            }
         //   BlackboardVariable<T> variable = enemy.GetBlackboardVariable<T>(variableName);
        }
    }
}

