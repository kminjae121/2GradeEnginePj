using System;
using System.Collections.Generic;
using System.Diagnostics;
using Blade.Combat;
using Blade.Core;
using Blade.Core.StatSystem;
using Blade.Entities;
using DG.Tweening;
using Member.Kmj._01.Scipt.Entity.AttackCompo;
using UnityEngine;
using UnityEngine.AI;
using Debug = UnityEngine.Debug;

namespace Blade.Enemies
{
    public class NavMovement : MonoBehaviour, IEntityComponet, IKnockBackable, IAfterInit
    {
        [field : SerializeField] public NavMeshAgent agent {get; private set;}
        
        private bool _isAirborne;
        private EntityStat _statCompo;

        [field : SerializeField] public LayerMask _whatIsGround { get; private set; }
        [SerializeField] private StatSO _moveSpeedStat;
        [SerializeField] private float stopOffset = 0.05f;
        [SerializeField] private float rotateSpeed = 10f;
        private Entity _entity;
        
        
        private float airborneTime = 0f;
        private float airborneDuration = 0.2f;
        private float jumpHeight = 3f;
        
        private Vector3 basePosition;
        
        public bool IsArrived => !agent.pathPending && agent.remainingDistance < agent.stoppingDistance + stopOffset;
        public float RemainDistance => agent.pathPending ? -1 : agent.remainingDistance;
        
        public void Initialize(Entity entity)
        {
            _entity = entity;
            _statCompo =  _entity.GetCompo<EntityStat>();
        }
        

        public void AfterInit()
        {
            StatSO targetSO = _statCompo.GetStat(_moveSpeedStat);
            targetSO.OnValudeChanged += HandleMoveSpeedChange; 
        }

        private void HandleMoveSpeedChange(StatSO stat, float currentValue, float previousValue)
        {
            agent.speed = currentValue;
        }

        private void OnDestroy()
        {
            _entity.transform.DOKill();
        }

        private void Update()
        {
            if (agent.hasPath && agent.isStopped == false && agent.path.corners.Length > 0)
            {
                LookAtTarget(agent.steeringTarget);
            }
        }
        

        /// <summary>
        /// 바라봐야할 최종 로테이션을 반환합니다.
        /// </summary>
        /// <param name="target">바라볼 목표지점을 넣습니다. y축은 무시</param>
        /// <param name="isSmooth">부드럽게 돌아갈 것인지 결정합니다.</param>
        /// <returns></returns>
        public Quaternion LookAtTarget(Vector3 target, bool isSmooth = true)
        {
            Vector3 direction = target - _entity.transform.position;
            direction.y = 0; 

            if (direction == Vector3.zero)
                return _entity.transform.rotation;
            
            Quaternion lookRotation = Quaternion.LookRotation(direction.normalized, Vector3.up);

            if (isSmooth)
            {
                _entity.transform.rotation = Quaternion.Slerp(_entity.transform.rotation, 
                    lookRotation, Time.deltaTime * rotateSpeed);
            }
            else
            {
                _entity.transform.rotation = lookRotation;
            }

            return lookRotation;
        }
        
        
        public void SetStop(bool isStop) => agent.isStopped = isStop;

        public void SetVelocity(Vector3 velocity) => agent.velocity = velocity; 
        public void SetSpeed(float speed) => agent.speed = speed;
        public void SetDestination(Vector3 destination) => agent.SetDestination(destination);

        private void LateUpdate()
        {
            transform.parent.GetChild(0).rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        }


        private Vector3 GetKnockBackEndPoint(Vector3 force)
        {
            Vector3 startPosition = _entity.transform.position + new Vector3(0, 0.5f); //위로 올려서 벽감지.
            if (Physics.Raycast(startPosition, force.normalized, out RaycastHit hit, force.magnitude))
            {
                Vector3 hitPoint = hit.point;
                hitPoint.y = _entity.transform.position.y;
                return hitPoint;
            }

            return _entity.transform.position + force;
        }
        
        public bool IsGrounded()
        {
            Vector3 origin = _entity.transform.position + Vector3.up * 0.1f;
            float distance = 0.2f; 

            bool hit = Physics.Raycast(origin, Vector3.down, out RaycastHit hitInfo, distance, _whatIsGround);
            
            print(hit);

            return hit;
        }

        public void KnockBack(Vector3 force, float duration)
        {
            SetStop(true); 
            Vector3 destination = GetKnockBackEndPoint(force);
            Vector3 delta = destination - _entity.transform.position; 
            float kbDuration = delta.magnitude * duration / force.magnitude;

            _entity.transform.DOMove(destination, kbDuration).SetEase(Ease.OutCirc)
                .OnComplete(() =>
                {
                    agent.Warp(transform.position);
                    SetStop(false);
                });
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, Vector3.down * 0.5f);
            Gizmos.color = Color.white;
        }
    }
    
}