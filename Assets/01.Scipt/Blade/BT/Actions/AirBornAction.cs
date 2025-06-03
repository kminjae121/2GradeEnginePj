using Blade.Enemies;
using System;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "AirBorn", story: "[Self] fast jump and fall", category: "Action", id: "7d66595fe3c2d8ee7e109ab9de392128")]
public partial class AirBornAction : Action
{
    [SerializeReference] public BlackboardVariable<Enemy> Self;

    private NavMeshAgent _agent;
    private Transform _transform;

    private float _verticalVelocity;
    private float _gravity = -30f;         // 중력 가속도
    private float _initialJumpVelocity = 10f;
    private float _currentHeight;
    private float _startY;
    private bool _isJumping = false;

    protected override Status OnStart()
    {
        var enemy = Self.Value;
        if (enemy == null) return Status.Failure;

        _agent = enemy.GetComponent<NavMeshAgent>();
        _transform = enemy.transform;

        if (_agent == null || _transform == null)
            return Status.Failure;

        _agent.enabled = false;
        _startY = _transform.position.y;
        _verticalVelocity = _initialJumpVelocity;
        _isJumping = true;

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (!_isJumping) return Status.Success;

        float deltaTime = Time.deltaTime;

        _verticalVelocity += _gravity * deltaTime;
        Vector3 pos = _transform.position;
        pos.y += _verticalVelocity * deltaTime;

        if (pos.y <= _startY)
        {
            // 바닥 도달
            pos.y = _startY;
            _transform.position = pos;
            _isJumping = false;
            return Status.Success;
        }

        _transform.position = pos;
        return Status.Running;
    }

    protected override void OnEnd()
    {
        if (_agent != null)
            _agent.enabled = true;
    }
}