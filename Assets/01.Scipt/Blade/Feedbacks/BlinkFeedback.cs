using DG.Tweening;
using UnityEngine;

namespace Blade.Feedbacks
{
    public class BlinkFeedback : Feedback
    {
        [SerializeField] private SkinnedMeshRenderer meshRenderer;
        
        [SerializeField] private float blinkDuration = 0.15f;
        [SerializeField] private float bliknIntensity = 1f;
        
        private readonly int _blinkHash = Shader.PropertyToID("_BlickValue");
        public override void CreateFeedback()
        {
         meshRenderer.material.SetFloat(_blinkHash, bliknIntensity);
         DOVirtual.DelayedCall(blinkDuration, StopFeedback);
        }

        public override void StopFeedback()
        {
            if (meshRenderer != null)
            {
                meshRenderer.material.SetFloat(_blinkHash, 0);
            }
        }
    }
}