using UnityEngine;

public class MouseBarrerSkill : SkillCompo
{
    [SerializeField] private GameObject _barrierEffect;

    private Player _player;

    public bool isPalling { get; set; }
    public override void GetSkill()
    {
        _player = _entity as Player;
        _barrierEffect.SetActive(false);
        _player.PlayerInput.OnSheldPressd += Skill;
        _player.PlayerInput.OnSheldCanceld += HandleBarrierCanceled;
    }

    public override void EventDefault()
    {
        _player.PlayerInput.OnSheldPressd -= Skill;
        _player.PlayerInput.OnSheldCanceld -= HandleBarrierCanceled;
    }

    protected override void Skill()
    {
        if (!_player._isSkilling && _player.isUseSheld)
        {
            _player.ChangeState("SHELD");
            _barrierEffect.SetActive(true);
            _player._isSkilling = true;
        }
    }

    public void HandleBarrierCanceled()
    {
        if (_player._isSkilling)
        {
            _player.ChangeState("IDLE");
            _player._isSkilling = false;
            _barrierEffect.SetActive(false);
        }
    }

    public void StopState()
    {
        _player.ChangeState("IDLE");
    }
}
