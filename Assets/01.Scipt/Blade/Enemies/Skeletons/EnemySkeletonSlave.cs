using System;
using Blade.BT.Events;
using Blade.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Blade.Enemies.Skeletons
{
    public class EnemySkeletonSlave : Enemy
    {
        private NavMovement _movement;
        public UnityEvent<Vector3,float> OnKnockBackEvent;
        private StateChange _StateChangeChannel;
        private CapsuleCollider _collider;

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
            IsDead = true;
            _collider.enabled = false;
            _StateChangeChannel.SendEventMessage(EnemyState.DEAD);
        }

        public void KnockBack(Vector3 force, float duration)
        {
            OnKnockBackEvent?.Invoke(force, duration);
        }
    }
}