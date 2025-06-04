using Blade.Enemies;
using System;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "EnemyIsStop", story: "[Self] is Stopped (drop if floating)", category: "Action", id: "b3b0108b84a9387278d5c1b121483833")]
public partial class EnemyIsStopAction : Action
{
    [SerializeReference] public BlackboardVariable<Enemy> Self;
    [SerializeField] private float stunDuration = 1.0f;
    [SerializeField] private float dropSpeed = 5f;
    [SerializeField] private float groundRayDistance = 2f;
    [SerializeField] private LayerMask groundLayer;

    private float timer;
    private NavMeshAgent agent;
    private Transform enemyTransform;
    private float groundY;
    private bool shouldDropAfterStun;

    protected override Status OnStart()
    {
        if (Self == null || Self.Value == null)
            return Status.Failure;

        var enemy = Self.Value;

        agent = enemy.GetComponent<NavMeshAgent>();
        enemyTransform = enemy.transform;
        
        if (agent != null)
        {
            agent.isStopped = true;
            agent.updatePosition = false;
            agent.updateRotation = false;
        }

        if (Physics.Raycast(enemyTransform.position + Vector3.up, Vector3.down, out var hit, groundRayDistance, groundLayer))
        {
            groundY = hit.point.y;
        }
        else
        {
            groundY = enemyTransform.position.y;
        }

        shouldDropAfterStun = false;
        timer = 0f;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        timer += Time.deltaTime;

        if (timer < stunDuration)
        {
            if (enemyTransform.position.y > groundY + 0.01f)
            {
                var pos = enemyTransform.position;
                pos.y = Mathf.Lerp(pos.y, groundY, Time.deltaTime * 10f);
                enemyTransform.position = pos;
            }

            return Status.Running;
        }

        if (enemyTransform.position.y > groundY + 0.05f)
        {
            shouldDropAfterStun = true;

            var pos = enemyTransform.position;
            pos.y -= dropSpeed * Time.deltaTime;
            if (pos.y < groundY) pos.y = groundY;

            enemyTransform.position = pos;
            return Status.Running;
        }

        return Status.Success;
    }

    protected override void OnEnd()
    {
        if (agent != null)
        {
            agent.isStopped = false;
            agent.updatePosition = true;
            agent.updateRotation = true;
        }
    }
}