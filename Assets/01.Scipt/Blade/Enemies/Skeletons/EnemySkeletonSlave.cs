using System;
using System.Collections;
using Blade.BT.Events;
using Blade.Effects;
using GondrLib.Dependencies;
using GondrLib.ObjectPool.Runtime;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using _01.Scipt.Core;
using Blade.Combat;
using Random = UnityEngine.Random;


namespace Blade.Enemies.Skeletons
{
    public class EnemySkeletonSlave : Enemy, IPoolable
    {
        private NavMovement _movement;
        public UnityEvent<Vector3,float> OnKnockBackEvent;
        private StateChange _StateChangeChannel;
        public EnemyState _State;
        private CapsuleCollider _collider;
        
        [SerializeField] private PoolingItemSO poolingTypeSO; // Pool 타입 명시

        private Pool _myPool;

        public PoolingItemSO PoolingType => poolingTypeSO;
        public GameObject GameObject => gameObject;
        
        [Inject]  private PoolManagerMono _poolManager;

        private PoolingEffect _enemy;


        private EntityHealth _healthCompo;
        
        protected override void Awake()
        {
            base.Awake();
            _collider = GetComponent<CapsuleCollider>();
            _healthCompo = GetComponent<EntityHealth>();
            OnDead.AddListener(HandleDeathEvent);
        }
        

        public override void Start()
        {
            base.Start();
            _StateChangeChannel = GetBlackboardVariable<StateChange>("StateChannel").Value;
            
            
            _collider.enabled = true;
            
            _StateChangeChannel.SendEventMessage(EnemyState.IDLE);
            
            IsDead = false;
        }

        private void OnDestroy()
        {
            OnDead.RemoveListener(HandleDeathEvent);
        }

        protected override void HandleHit()
        {
            
        }

        protected override void HandleDead()
        {
           
        }

        protected override void HandleStun()
        {
            
        }

        public void HandleStop()
        {
            _StateChangeChannel.SendEventMessage(EnemyState.STOP);
        }

        public void HandleHaveToStun()
        {
            _StateChangeChannel.SendEventMessage(EnemyState.STUN);
        }

        public void HandleJumpAndStun()
        {
            AudioManager.Instance.PlayHitSFX("HitSound");
            
            _StateChangeChannel.SendEventMessage(EnemyState.JUMPANDSTUN);
        }

        private void HandleDeathEvent()
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

                StartCoroutine(WaitDie());
            }
        }

        public void KnockBack(Vector3 force, float duration)
        {
            OnKnockBackEvent?.Invoke(force, duration);
        }

        public void ChangeJumpChannelEvent()
        {
            AudioManager.Instance.PlayHitSFX("HitSound");
            _StateChangeChannel.SendEventMessage(EnemyState.AIRBORN);
        }
        public void ChangeHitChannelEvent()
        {
            AudioManager.Instance.PlayHitSFX("HitSound");
            if (_State == EnemyState.AIRBORN)
            {
                _StateChangeChannel.SendEventMessage(EnemyState.AIRBORN);
            }
            else
            {
                _StateChangeChannel.SendEventMessage(EnemyState.HIT);   
            }
        }

        private IEnumerator WaitDie()
        {
            //_StateChangeChannel.SendEventMessage(EnemyState.DEAD);
            yield return new WaitForSeconds(1.3f);
            GoodsManager.Instance.GetCoin(2);
            EnemyDieCompo.Instance.PushEnemy(this);
        }
        
        public void SetUpPool(Pool pool)
        {
            _myPool = pool;
        }

        public void ResetItem()
        {
            
        }   
    }
}