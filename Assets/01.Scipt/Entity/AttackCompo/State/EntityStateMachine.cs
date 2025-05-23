using System;
using System.Collections.Generic;
using Member.Kmj._01.Scipt.Entity.AttackCompo;
using UnityEngine;

public class EntityStateMachine
{
    public EntityState CurrentState { get; set; }
    private Dictionary<string, EntityState> _states;

    public EntityStateMachine(Entity entity, StateDataSO[] stateList)
    {
        _states = new Dictionary<string, EntityState>();
        foreach (StateDataSO state in stateList)
        {
            Type type = Type.GetType(state.className);
            Debug.Assert(type != null, $"Finding type is null : {state.className}");
            EntityState entityState = Activator.CreateInstance(type, entity, state.animationHash) as EntityState;
            _states.Add(state.stateName, entityState);
        }
    }

    public void ChangeState(string newStateName, bool forced = false)
    {
        CurrentState?.Exit();
        EntityState newState = _states.GetValueOrDefault(newStateName);
        Debug.Assert(newState != null, $"State in null : {newStateName}");

        if (forced == false && CurrentState == newState)
            CurrentState = CurrentState;

        CurrentState = newState;
        CurrentState.Enter();
    }

    public void UpdateStateMachine()
    {
        CurrentState?.Update();
    }
}
