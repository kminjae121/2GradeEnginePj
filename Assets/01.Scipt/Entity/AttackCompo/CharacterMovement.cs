
using System;
using _01.Scipt.Player.Player;
using Blade.Core.StatSystem;
using Member.Kmj._01.Scipt.Entity.AttackCompo;
using UnityEngine;

public class CharacterMovement : MonoBehaviour, IEntityComponet, IAfterInit
{
    [field: SerializeField] public Rigidbody _rbcompo { get; private set; }
    [SerializeField] private float rotationSpeed = 8f;
    private float moveSpeed;
    private float rollingSpeed;
    public bool CanMove { get; set; } = true;

    private EntityAnimator _animatorCompo;
    private EntityAnimatorTrigger _triggerCompo;
    private Vector3 _autoMovement;

    public Vector3 _velocity { get; set; }
    public Vector3 Velocity => _velocity;

    public bool CanManualMovement { get; set; } = true;
    

    public bool IsRolling { get; set; } = false;
    private Player _entity;
    
    Vector2 currentBlend;
    [SerializeField] float blendSpeed = 10f;

    [SerializeField] private StatSO _moveSpeedStat;
    [SerializeField] private StatSO _rollingSpeedStat;
    [SerializeField] private EntityStat _stat;
    private Vector3 _movementDirection;

    public void Initialize(Entity entity)
    {
        _entity = entity as Player;
    }

    private void Start()
    {
        _triggerCompo = _entity.GetCompo<EntityAnimatorTrigger>();
        _animatorCompo = _entity.GetCompo<EntityAnimator>();
        moveSpeed = _stat.GetStat(_moveSpeedStat).Value;
        _triggerCompo.OnMove += PlayWalkSound;
    }

    private void OnDestroy()
    {
        _triggerCompo.OnMove -= PlayWalkSound;
    }


    public void SetMove(float XMove, float ZMove)
    {
        if (_entity._isSkilling == false)
        {
            _movementDirection.x = XMove;
            _movementDirection.z = ZMove;
        }
    }

    public void PlayWalkSound()
    {
        AudioManager.Instance.PlaySFX("WalkSound",0.2f);
    }
    private void Update()
    {
        Vector2 rawInput = _entity.PlayerInput.MovementKey.normalized;
        
        currentBlend = Vector2.Lerp(currentBlend, rawInput, Time.deltaTime * blendSpeed);
        
        _animatorCompo.animator.SetFloat("Horizon", currentBlend.x);
        _animatorCompo.animator.SetFloat("Vertical", currentBlend.y);
    }

    private void FixedUpdate()
    {
        if (_entity._isSkilling == false)
        {
            CalculateMovement();
        
            _rbcompo.linearVelocity = new Vector3(transform.TransformDirection(_velocity).x,
                _rbcompo.linearVelocity.y, transform.TransformDirection(_velocity).z);   
        }
    }

    public void MoveToEntity(Vector3 target)
    {
        _entity.transform.position =
           Vector3.MoveTowards(_entity.transform.position, target, 60 * Time.deltaTime);
    }
    
    public void LookAt(Vector3 entity)
    {
        Vector3 targetPos = entity;
        Vector3 direction = targetPos - transform.position;
        direction.y = 0;

        transform.rotation = Quaternion.LookRotation(direction.normalized);
    }
    private void CalculateMovement()
    {
        if (CanMove && _entity._isSkilling == false) 
        {

            if (CanManualMovement)
            {
                _velocity = Quaternion.Euler(0, 0, 0) * _movementDirection;
                _velocity *= moveSpeed;
            }
            
            else
            {
                _velocity = _autoMovement * Time.fixedDeltaTime;
            }

            if(IsRolling)
            {
                _velocity = Quaternion.Euler(0, 0f, 0) * _movementDirection;
                _velocity *= rollingSpeed;
            }
            
        }
    }
    

    public void StopImmediately()
    {
        _velocity = Vector3.zero;
    }

    public void AfterInit()
    {
        
    }
}
