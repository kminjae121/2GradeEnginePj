using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "PlayerInputSO", menuName = "Scriptable Objects/PlayerInputSO")]
public class PlayerInputSO : ScriptableObject, Controls.IPlayerActions
{
    [SerializeField] private LayerMask whatIsGround;

    public event Action OnAttackPressd,
        OnInteracetPressd,
        OnSheldPressd,
        OnRollingPressed,
        OnSheldCanceld,
        OnChargeAttackPressed,
        OnChargeAttackCanceled,
        OnStrongAttackPressed;
    

    private Controls _control;
    private Vector2 _screenPos;
    private Vector3 _worldPos;

    public Vector2 MovementKey { get; set; }

    private void OnEnable()
    {
        if (_control == null)
        {
            _control = new Controls();
            _control.Player.SetCallbacks(this);
        }

        _control.Player.Enable();
    }

    private void OnDisable()
    {
        _control.Player.Disable();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnAttackPressd?.Invoke();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnInteracetPressd?.Invoke();
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        var movementKey = context.ReadValue<Vector2>();
        MovementKey = movementKey;
    }

    public void OnMousePos(InputAction.CallbackContext context)
    {
        _screenPos = context.ReadValue<Vector2>();
    }

    public void OnSheldSkill(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnSheldPressd?.Invoke();
        else if (context.canceled)
            OnSheldCanceld?.Invoke();
    }

    public void OnRolling(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnRollingPressed?.Invoke();
    }

    public void OnStrongAttackSkill(InputAction.CallbackContext context)
    {
        if(context.performed)
            OnStrongAttackPressed?.Invoke();
    }


    public void OnChargeSklil(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnChargeAttackPressed?.Invoke();
        else if (context.canceled)
            OnChargeAttackCanceled?.Invoke();
    }
    

    public Vector3 GetWorldPosition(out RaycastHit hit)
    {
        var main = Camera.main;
        Debug.Assert(main != null, "No main camera in this scene");

        var cameraRay = main.ScreenPointToRay(_screenPos);
        if (Physics.Raycast(cameraRay, out hit, main.farClipPlane, whatIsGround)) _worldPos = hit.point;
        return _worldPos;
    }
}