using Member.Kmj._01.Scipt.Entity.AttackCompo;
using UnityEngine;

public class PlayerDieState : PlayerState
{
    public PlayerDieState(Entity entity, int animationHash) : base(entity, animationHash)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _player._movement.StopImmediately();
        _player._movement.CanMove = false;
        _player.GetComponent<Collider>().enabled = false;
        _player.GetComponent<Rigidbody>().useGravity = false;
    }

    public override void Update()
    {
        base.Update();
        if (_isTriggerCall)
        {
            _player.FailUI.SetActive(true);
            GameTimeManager.Instance.overrideTime();
            Time.timeScale = 0;
            Cursor.visible = true;         
            Cursor.lockState = CursorLockMode.None;
            PlayerComboSystem.Instance.CURRENTComboStr = "D";
            PlayerComboSystem.Instance._CurrentComboStat = 0;
            _player.gameObject.SetActive(false);
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
}
