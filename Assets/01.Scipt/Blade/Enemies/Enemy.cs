using System;
using Blade.Entities;
using GondrLib.Dependencies;
using GondrLib.ObjectPool.Runtime;
using Member.Kmj._01.Scipt.Entity.AttackCompo;
using Unity.Behavior;
using UnityEngine;

namespace Blade.Enemies
{
    public abstract class Enemy : Entity,IPoolable
    {
        [field: SerializeField ] public PoolingItemSO PoolingType { get; private set; }
        [field: SerializeField] public EntityFinderSO PlayerFinder { get; protected set; }
        
        protected BehaviorGraphAgent _btAgent;
        public BehaviorGraphAgent BTEnemy => _btAgent;
        public GameObject GameObject => gameObject;
        
        [Inject]  private PoolManagerMono _poolManager;
        
        [Header("PoolItem")]
        [SerializeField] private GameObject poolEnemy;
        private Pool _myPool;
        public string PoolingName { get; }
        
        #region Temp region
        [Header("Temp Range")]
        public float detectRange;
        [Space(10f)]
        public float attackRange;

        #endregion

        private void Start()
        {
            BlackboardVariable<Transform> target = GetBlackboardVariable<Transform>("Target");
            target.Value = PlayerFinder.Target.transform; //플레이어를 타겟으로 넣어준다. 
            OnDead.AddListener(AddPool);
        }
        
        public void SetUpPool(Pool pool)
        {
            _myPool = pool;
        }

        public void ResetItem()
        {
            
        }

        private void AddPool()
        {
            _poolManager.Push(this);
        }

        public override void AddComponets()
        {
            base.AddComponets();
            _btAgent = GetComponent<BehaviorGraphAgent>();
            Debug.Assert(_btAgent != null, $"{gameObject.name} does not have an BehaviorGraphAgent");
        }


        public BlackboardVariable<T> GetBlackboardVariable<T>(string key)
        {
            if (_btAgent.GetVariable(key, out BlackboardVariable<T> result))
                return result;
            return default;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, detectRange);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
        
    }
}