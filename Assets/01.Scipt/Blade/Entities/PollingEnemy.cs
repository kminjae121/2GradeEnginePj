using _01.Scipt.Effect;
using GondrLib.ObjectPool.Runtime;
using UnityEngine;

namespace _01.Scipt.Blade.Entities
{
    public class PollingEnemy : MonoBehaviour, IPoolable
    {
        [field: SerializeField ] public PoolingItemSO PoolingType { get; private set; }
        public GameObject GameObject => gameObject;

        private Pool _myPool;
        [SerializeField] private GameObject enemy;


        private void Awake()
        {
            
        }

        public void SetUpPool(Pool pool)
        {
            _myPool = pool;
        }

        public string PoolingName { get; }
        

        public void ResetItem()
        {
            
        }
        
    }
}
