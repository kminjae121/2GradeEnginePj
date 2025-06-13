using System.Collections;
using _01.Scipt.Core;
using Blade.BT.Events;
using Blade.Combat;
using Blade.Enemies;
using Blade.Enemies.Skeletons;
using UnityEngine;

public class EnemyBossSkeleton : Enemy
{
    private NavMovement _movement;
    private StateChange _StateChangeChannel;
    public EnemyState _State;
    private CapsuleCollider _collider;
    private EntityHealth _healthCompo;
    
    protected override void Awake()
    {
        base.Awake();
        _collider = GetComponent<CapsuleCollider>();
        _healthCompo = GetComponent<EntityHealth>();
        OnDead.AddListener(HandleDead);
    }
        

    public override void Start()
    {
        base.Start();
        _StateChangeChannel = GetBlackboardVariable<StateChange>("StateChannel").Value;
            
            
        _collider.enabled = true;
            
        _StateChangeChannel.SendEventMessage(EnemyState.IDLE);
        OnHit.AddListener(HandleHit);
            
        IsDead = false;
    }

    private void OnDestroy()
    {
        OnDead.RemoveListener(HandleDead);
        OnHit.RemoveListener(HandleHit);
    }

    
    protected override void HandleHit()
    {
        AudioManager.Instance.PlayHitSFX("HitSound");
        _StateChangeChannel.SendEventMessage(EnemyState.HIT);  
    }

    protected override void HandleDead()
    {
        if (IsDead) return;
        if (_healthCompo.currentHealth <= 0)
        {
            GameManager.instance.GetExp();
            GameManager.instance.killCount++;
            IsDead = true;
            int random = Random.Range(0, 100);


            if (GameManager.instance.GetBallPercent == 0)
            {
            }
            else if (random <= GameManager.instance.GetBallPercent)
            {
                GameManager.instance.SpawnHpBall(transform.position, Quaternion.identity);
            }

            _collider.enabled = false;

            _StateChangeChannel.SendEventMessage(EnemyState.DEAD);

            StartCoroutine(WaitDie());
        }
    }

    private IEnumerator WaitDie()
    {
        yield return new WaitForSeconds(1.3f);
        GoodsManager.Instance.GetCoin(2);
    }
    
    protected override void HandleStun()
    {
       
    }
}
