using Blade.Enemies;
using System;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "AirBorn", story: "[Self] fast jump and fall (with hover)", category: "Action", id: "7d66595fe3c2d8ee7e109ab9de392128")]
public partial class AirBornAction : Action
{
    [SerializeReference] public BlackboardVariable<Enemy> Self;

    private NavMeshAgent _agent;
    private Transform _transform;

    private float _verticalVelocity;
    private float _gravity = -28f;   
    private float _jumpPower = 7f;    
    private float _startY;

    private bool _isJumping = false;
    private bool _isHovering = true;
    private float _hoverTime = 4f;   
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
        _isHovering = true;
        _hoverTimer = 0f;

        return Status.Running;
    }
    

    protected override Status OnUpdate()
    {
        if (!_isJumping)
            return Status.Success;

        float deltaTime = Time.deltaTime;
        Vector3 pos = _transform.position;

        if (_isHovering)
        {
            _hoverTimer += deltaTime;
        
            // 수직 속도 적용 없이 위치 유지 (정지)
            if (_hoverTimer >= _hoverTime)
            {
                _isHovering = false;
            }
        }
        else
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

        _transform.position = pos;
        return Status.Running;
    }
    protected override void OnEnd()
    {
        if (_agent != null)
            _agent.enabled = true;
    }
}