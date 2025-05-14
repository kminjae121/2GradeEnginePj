
using Blade.Effects;
using Blade.Entities;
using Gondrlib.Dependencies;
using GondrLib.ObjectPool.Runtime;
using UnityEngine;
using DG.Tweening;

namespace Feedbacks
{
    public class HitImpactFeedback : Feedback
    {

        [SerializeField] private PoolingItemSO hitImpactItem;
        [SerializeField] private float playDuration = 0.5f;
        [SerializeField] private ActionData actionData;

        [Inject] private PoolManagerMono _poolManager;

        private PoolingEffect _effect;
        
        public override void CreateFeedback()
        {
            _effect = _poolManager.Pop<PoolingEffect>(hitImpactItem);
            
            Quaternion rotation = Quaternion.LookRotation(actionData.HitNormal);
            _effect.PlayVFX(actionData.HitPoint, rotation);
            
            DOVirtual.DelayedCall(playDuration,StopFeedback);
        }

        public override void StopFeedback()
        {
            if (_effect != null) return;
            _poolManager.Push(_effect);
        }
    }
}