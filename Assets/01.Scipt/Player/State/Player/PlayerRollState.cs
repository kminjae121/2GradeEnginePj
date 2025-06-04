using Member.Kmj._01.Scipt.Entity.AttackCompo;
using UnityEngine;

public class PlayerRollState : PlayerState
{
    private readonly float _rollDuration = 0.6f;
    private readonly float _rollForce;
    private readonly Transform _camPos;

    private float _elapsedTime;
    private Vector3 _rollDirection;
    private Rigidbody _rigidbody;

    public PlayerRollState(Entity entity, int animationHash) : base(entity, animationHash)
    {
        
    }

    public override void Enter()
    {
        base.Enter();

        _elapsedTime = 0f;
        _rigidbody = _player.GetComponent<Rigidbody>();
        _rigidbody.linearVelocity = Vector3.zero;
        
        Vector2 input = _player.PlayerInput.MovementKey;
        
        if (input == Vector2.zero)
        {
            _rollDirection = _player.transform.forward;
        }
        else
        {
            Vector3 camForward = _camPos.forward;
            Vector3 camRight = _camPos.right;
            camForward.y = 0f;
            camRight.y = 0f;
            _rollDirection = (camForward * input.y + camRight * input.x).normalized;
        }
        
        _rigidbody.AddForce(_rollDirection * _rollForce, ForceMode.VelocityChange);
    }

    public override void Update()
    {
        base.Update();

        _elapsedTime += Time.deltaTime;
        

        if (_elapsedTime >= _rollDuration)
        {
            _player.ChangeState("IDLE");
        }
    }

    public override void Exit()
    {
        base.Exit();
        
        _rigidbody.linearVelocity = Vector3.zero;
    }
}