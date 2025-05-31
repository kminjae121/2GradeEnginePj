using System;
using System.Collections;
using Blade.BT.Events;
using Blade.Effects;
using GondrLib.Dependencies;
using GondrLib.ObjectPool.Runtime;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using Random = UnityEngine.Random;


namespace Blade.Enemies.Skeletons
{
    public class EnemySkeletonSlave : Enemy
    {
        private NavMovement _movement;
        public UnityEvent<Vector3,float> OnKnockBackEvent;
        private StateChange _StateChangeChannel;
        private CapsuleCollider _collider;

        [SerializeField] private PoolingItemSO hitImpactItem;
        
        [Inject]  private PoolManagerMono _poolManager;

        private PoolingEffect _enemy;
        

        protected override void Awake()
        {
            base.Awake();
            _collider = GetComponent<CapsuleCollider>();
            OnDead.AddListener(HandleDeathEvent);
        }
        

        protected override void Start()
        {
            base.Start();
            _StateChangeChannel = GetBlackboardVariable<StateChange>("StateChannel").Value;
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

        private void HandleDeathEvent()
        {
            if (IsDead) return;
            GameManager.instance.GetExp();
            GameManager.instance.killCount++;
            IsDead = true;
            int random = Random.Range(0, 100);
            
            print(random);
            print(GameManager.instance.GetBallPercent);
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

        public void KnockBack(Vector3 force, float duration)
        {
            OnKnockBackEvent?.Invoke(force, duration);
        }

        public void ChangeJumpChannelEvent()
        {
            _StateChangeChannel.SendEventMessage(EnemyState.AIRBORN);
        }
        public void ChangeHitChannelEvent()
        {   
            _StateChangeChannel.SendEventMessage(EnemyState.HIT);
        }

        private IEnumerator WaitDie()
        {
            yield return new WaitForSeconds(1.3f);
            
            gameObject.SetActive(false);
        }

        public PoolingItemSO PoolingType { get; }
        public GameObject GameObject { get; }
        
    }
}