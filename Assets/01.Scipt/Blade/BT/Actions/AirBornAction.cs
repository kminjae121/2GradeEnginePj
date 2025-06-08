using Blade.Enemies;
using System;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "AirBorn", story: "[Self] jump and hover at apex before falling", category: "Action", id: "7d66595fe3c2d8ee7e109ab9de392128")]
public partial class AirBornAction : Action
{
    [SerializeReference] public BlackboardVariable<Enemy> Self;

    private NavMeshAgent _agent;
    private Transform _transform;

    private float _verticalVelocity;
    private float _gravity = -7f;
    private float _jumpPower = 5f;

    private float _startY;
    private bool _isJumping = false;
    private bool _isHovering = false;
    private bool _isFalling = false;

    private float _hoverDuration = 0.4f;
    private float _hoverTimer = 0f;

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
        _verticalVelocity = _jumpPower;

        _isJumping = true;
        _isHovering = false;
        _isFalling = false;
        _hoverTimer = 0f;

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (!_isJumping)
            return Status.Success;

        float deltaTime = Time.deltaTime;
        Vector3 pos = _transform.position;

        if (!_isHovering && !_isFalling)
        {
            _verticalVelocity += _gravity * deltaTime;
            pos.y += _verticalVelocity * deltaTime;
            
            if (_verticalVelocity <= 0f)
            {
                _isHovering = true;
                _verticalVelocity = 0f;
            }
        }
        else if (_isHovering)
        {
            _hoverTimer += deltaTime;

            if (_hoverTimer >= _hoverDuration)
            {
                _isHovering = false;
                _isFalling = true;
                _verticalVelocity = 0f; 
            }
        }
        else if (_isFalling)
        {
            _verticalVelocity += _gravity * deltaTime;
            pos.y += _verticalVelocity * deltaTime;

            if (pos.y <= _startY)
            {
                pos.y = _startY;
                _transform.position = pos;
                _isJumping = false;
                return Status.Success;
            }
        }

        Self.Value.transform.position = pos;
        return Status.Running;
    }

    protected override void OnEnd()
    {
        if (_agent != null)
            _agent.enabled = true;
    }
}