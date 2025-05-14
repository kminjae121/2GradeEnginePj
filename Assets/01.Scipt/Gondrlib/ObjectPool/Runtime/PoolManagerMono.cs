using Gondrlib.Dependencies;
using UnityEngine;

namespace GondrLib.ObjectPool.Runtime
{
    [Provide]
    public class PoolManagerMono : MonoBehaviour
    {
        [SerializeField] private PoolManagerSO poolManage;

        private void Awake()
        {
            poolManage.Initialize(transform);
        }

        public T Pop<T>(PoolingItemSO item) where T : IPoolable
        {
            return (T)poolManage.Pop(item);
        }
        
        public void Push(IPoolable target)
        {   poolManage.Push(target);
        }
    }
}