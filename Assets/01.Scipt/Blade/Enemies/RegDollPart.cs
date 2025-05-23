using System.Drawing;
using DG.Tweening;
using UnityEngine;

namespace Blade.Enemies
{
    public class RegDollPart : MonoBehaviour
    {
        private Rigidbody _rbcompo;
        private Collider _colliderCompo;

        public void Initialize()
        {
            _rbcompo = GetComponent<Rigidbody>();
            _colliderCompo = GetComponent<Collider>();
        }

        public void SetRegDollActive(bool active)
        {
            _rbcompo.isKinematic = !active;
        }

        public void SetCollider(bool isActive)
        {
            _colliderCompo.enabled = isActive;
        }

        public void KnockBack(Vector3 force, Vector3 point)
        {
            DOVirtual.DelayedCall(0.1f, () =>
            {
                _rbcompo.AddForceAtPosition(force, point, ForceMode.Impulse);
            });
        }
    }
}