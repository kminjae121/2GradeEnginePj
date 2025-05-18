using System;
using _01.Scipt.Effect;
using GondrLib.ObjectPool.Runtime;
using UnityEngine;

namespace Blade.Effects
{
    public class PoolingEffect : MonoBehaviour, IPoolable
    {
        [field: SerializeField ] public PoolingItemSO PoolingType { get; private set; }
        public GameObject GameObject => gameObject;

        private Pool _myPool;
        [SerializeField] private GameObject effectObject;
        private IPlayableVFX _playableVfx;

        private void Awake()
        {
            
        }

        public void SetUpPool(Pool pool)
        {
            _myPool = pool;
            _playableVfx = effectObject.GetComponent<IPlayableVFX>();
        }

        public string PoolingName { get; }
        

        public void ResetItem()
        {
            
        }

        private void OnValidate()
        {
            if (effectObject == null) return;   
            _playableVfx = effectObject.GetComponent<IPlayableVFX>();
            
            if (_playableVfx == null)
            {
                Debug.LogError("Error");
                effectObject = null;
            }
        }

        public void PlayVFX(Vector3 actionDataHitPoint, Quaternion rotation)
        {
            _playableVfx.PlayVfx(actionDataHitPoint, rotation);
        }
    }
}