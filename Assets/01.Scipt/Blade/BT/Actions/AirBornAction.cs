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
    private float _gravity = -15f;
    private float _jumpPower = 5.5f;

    private float _startY;
    private bool _isJumping = false;
    private bool _isHovering = false;
    private bool _isFalling = false;

    
    private float _hoverY = 0f;
    private float _hoverDuration = 0.3f;
    private float _hoverTimer = 0f;
    
    private bool _wasHit = false;

    protected override Status OnStart()
    {
        var enemy = Self.Value;
        if (enemy == null) return Status.Failure;

        _agent = enemy.GetComponent<NavMeshAgent>();
        _transform = enemy.transform;

        if (_agent == null || _transform == null)
            return Status.Failure;

        _agent.enabled = false;

        _startY = Self.Value.transform.position.y;

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

        if (!_isHovering && !_isFalling)
        {
            _transform.position += Vector3.up * _verticalVelocity * deltaTime;
            
            _verticalVelocity += _gravity * deltaTime;

            if (_verticalVelocity <= 0f)
            {
                _isHovering = true;
                _verticalVelocity = 0f;
                
                _hoverY = _transform.position.y;
            }
        }
        else if (_isHovering)
        {
            
            Vector3 pos = _transform.position;
            pos.y = _hoverY;
            _transform.position = pos;

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
            _transform.position += Vector3.up * _verticalVelocity * deltaTime;

            if (_transform.position.y <= _startY)
            {
                Vector3 pos = _transform.position;
                pos.y = _startY;
                _transform.position = pos;

                _isJumping = false;
                return Status.Success;
            }
        }

        return Status.Running;
    }
    protected override void OnEnd()
    {
        if (_agent != null)
            _agent.enabled = true;
    }
}