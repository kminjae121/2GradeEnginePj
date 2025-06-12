using System;
using System.Collections;
using System.Collections.Generic;
using _01.Scipt.Player.Player;
using Member.Kmj._01.Scipt.Entity.AttackCompo;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFuryManager : MonoBehaviour, IEntityComponet
{
    public static PlayerFuryManager Instance;

    public float _Currentfury { get; set; }

    public float maxFury { get; set; } = 250f;
    
    private float fury;

    [field: SerializeField] public Slider _furySlider { get; private set; }
    
    private Coroutine _furyCoroutine;

    private EntityVFX _vfxCompo;
    public float reduceAmount { get; set; } = 0.76f;

    private EntityAnimatorTrigger _triggerCompo;
    public bool isInRange { get; set; }

    private Player _player;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        
        _furySlider.maxValue = maxFury;
        
        fury = 0f;
    }
    public void Initialize(Entity entity)
    {
        _player = entity as Player;
        _triggerCompo = entity.GetCompo<EntityAnimatorTrigger>();
        _vfxCompo = entity.GetCompo<EntityVFX>();
        _triggerCompo.OnShakingCamTriegger += ChangeFuryShaking;
    }


    private void OnDisable()
    {
        _triggerCompo.OnShakingCamTriegger -= ChangeFuryShaking;
    }

    private void Update()
    {
        fury = Mathf.Lerp(fury, _Currentfury, Time.deltaTime);
        _furySlider.value = fury;
    }

    public void RaiseFury(float amount)
    {
        if (isInRange) return; 

        _Currentfury += amount;
        _Currentfury = Mathf.Clamp(_Currentfury, 0, maxFury);

        if (_furyCoroutine != null)
            StopCoroutine(_furyCoroutine); 

        _furyCoroutine = StartCoroutine(ReduceFury());
        
        if (_Currentfury >= maxFury)
        {
            EnterRageState();
        }
    }

    private IEnumerator ReduceFury()
    {
        yield return new WaitForSeconds(3f); 

        while (_Currentfury > 0)
        {
            _Currentfury -= reduceAmount;
            _furySlider.value = _Currentfury;
            yield return new WaitForSeconds(0.1f);
        }

        _furyCoroutine = null;
    }

    public void ChangeFuryShaking()
    {
        _vfxCompo.PlayVfx("FuryEffect",transform.position,transform.rotation);
        CameraShakingManager.instance.ShakeCam(0.3f,0.5f,20, 30);
    }

    private void EnterRageState()
    {
        isInRange = true;
        
        _player._movement._rbcompo.linearVelocity = Vector3.zero;
        _player._movement.StopImmediately();
        _player._movement.CanMove = false;
        _player.ChangeState("FURY");
        AudioManager.Instance.PlaySFX("PoHo", 0.3f);
        
        StartCoroutine(RageDuration(5f));
    }

    private IEnumerator RageDuration(float duration)
    {
        yield return new WaitForSeconds(duration);

        ExitRageState();
    }
    
    
    

    private void ExitRageState()
    {
        isInRange = false;
        _Currentfury = 0;
        _furySlider.value = 0;
        _player._attackCompo.atkDamage = _player._attackCompo.baseAtkDamage;
        _player._skillCompo.skillDamage = _player._skillCompo.BaseskillDamage;
        
        _player._movement._rbcompo.linearVelocity = Vector3.zero;
        _player._movement.StopImmediately();
        _player._movement.CanMove = true;
        _vfxCompo.StopVfx("FuryEffect");
    }
    
    
    public bool IsInRage => isInRange;

}
