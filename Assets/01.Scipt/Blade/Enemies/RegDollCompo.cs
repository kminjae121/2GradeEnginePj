using System.Collections.Generic;
using System.Linq;
using Blade.Entities;
using Member.Kmj._01.Scipt.Entity.AttackCompo;
using UnityEngine;

namespace Blade.Enemies
{
    public class RagDollCompo : MonoBehaviour, IEntityComponet
    {
        [SerializeField] private Transform ragDollParentTrm;
        [SerializeField] private LayerMask whatIsBody;

        private List<RegDollPart> _partList;
        private Collider[] _results;

        private RegDollPart _defaultPart;

        private ActionData _actionData;
        public void Initialize(Entity entity)
        {
            _actionData = entity.GetCompo<ActionData>();
            _results = new Collider[1]; //길이 하나짜리 충돌 배열
            _partList = ragDollParentTrm.GetComponentsInChildren<RegDollPart>().ToList();
            foreach (RegDollPart part in _partList)
            {
                part.Initialize(); //각 파츠들 초기화
            }
            Debug.Assert(_partList.Count > 0, $"No ragdoll part found in {gameObject.name}");
            _defaultPart = _partList[0]; //기본 파츠
            SetRagDollActive(false);
            SetColliderActive(false);
            
            entity.OnDead.AddListener(HandleDeathEvent);
        }

        private void HandleDeathEvent()
        {
            SetColliderActive(true);
            SetRagDollActive(true);
            const float force = -30f;
            AddForceToRagDoll(_actionData.HitNormal * force, _actionData.HitPoint);
        }
        private void SetRagDollActive(bool isActive)
        {
            foreach (RegDollPart part in _partList)
            {
                part.SetRegDollActive(isActive);
            }
        }

        private void SetColliderActive(bool isActive)
        {
            foreach (RegDollPart part in _partList)
            {
                part.SetCollider(isActive);
            }
        }

        public void AddForceToRagDoll(Vector3 force, Vector3 point)
        {
            int count = Physics.OverlapSphereNonAlloc(point, 0.5f, _results, whatIsBody);
            if (count > 0)
            {
                _results[0].GetComponent<RegDollPart>().KnockBack(force, point);
            }
            else
            {
                _defaultPart.KnockBack(force, point);
            }
            
        }
    }
}