using System;
using Member.Kmj._01.Scipt.Entity.AttackCompo;
using UnityEngine;

public class EntityAnimatorTrigger : MonoBehaviour, IEntityComponet
{
    
    public Action OnAnimationEndTrigger;
    public Action OnAttackTrigger;

    public Action OnAttackTriggerEnd, OnSwingAttackTrigger;

    public event Action OnAttackFinalVFXTrigger;
    public event Action OnAttackVFXTrigger;

    public event Action OnPowerAttackVFXTrigger;

    public event Action LastAttackEffectEndTrigger;

    public event Action OnStrongAttackTrigger;

    public event Action PowerAttackTrigger;

    public event Action OnBarrierPressed;
    public event Action OnAttackDash;

    public event Action OnAttackAnimEnd;

    public event Action OnAttackCancel;

    public event Action<bool> OnRollingStatusChange;

    public event Action<bool> OnManualRotationTrigger;

    public event Action OnHighAttack;

    private Entity _entity;

    public event Action OnAttackMoveTrigger;

    public void Initialize(Entity entity)
    {
        _entity = entity;
    }

    private void AnimationEnd()
    {
        OnAnimationEndTrigger?.Invoke();
    }

    private void AttackMove() => OnAttackMoveTrigger?.Invoke();

    private void AttackDash() => OnAttackDash?.Invoke();
    private void RollingStart() => OnRollingStatusChange?.Invoke(true);
    private void RollingEnd() => OnRollingStatusChange?.Invoke(true);

    private void PlayAttackVFX() => OnAttackVFXTrigger?.Invoke();

    private void PlayerStrongAttack() => OnStrongAttackTrigger?.Invoke();
    private void AttackEnd() => OnAttackTriggerEnd?.Invoke();

    private void FinalAttack() => OnAttackFinalVFXTrigger?.Invoke();
    
    private void SwingAttack() => OnSwingAttackTrigger?.Invoke();
    private void BarrierPressed() => OnBarrierPressed?.Invoke();

    private void NextAttackTrue() => OnAttackAnimEnd?.Invoke();
    private void CancelNextAttack() => OnAttackCancel?.Invoke();

    private void HighAttack() => OnHighAttack?.Invoke();

    private void PowerAttack() => PowerAttackTrigger?.Invoke();
    private void EndLastAttackEffect() => LastAttackEffectEndTrigger?.Invoke();

    private void PowerAttackEffect() => OnPowerAttackVFXTrigger?.Invoke();

    private void Attack()
    {
        OnAttackTrigger?.Invoke();
    }

    private void StartManualRotation() => OnManualRotationTrigger?.Invoke(true);
    private void StopManualRotation() => OnManualRotationTrigger?.Invoke(false);
}
