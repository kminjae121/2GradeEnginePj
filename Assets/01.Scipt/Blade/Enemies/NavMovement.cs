using Blade.Entities;
using System;
using Member.Kmj._01.Scipt.Entity.AttackCompo;
using UnityEngine;
using UnityEngine.AI;

namespace Blade.Enemies
{
    public class NavMovement : MonoBehaviour, IEntityComponet
    {
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private float moveSpeed = 4f;
        [SerializeField] private float stopOffset = 0.05f; //거리에 대한 오프셋

        private const float RotateSpeed = 10f;
        private Entity _entity;
        
        public bool IsArrived => !agent.pathPending && agent.remainingDistance < agent.stoppingDistance + stopOffset;
        public float RemainDistance => agent.pathPending ? -1 : agent.remainingDistance;
        
        public void Initialize(Entity entity)
        {
            _entity = entity;
            agent.speed = moveSpeed;

            
        }

        private void Update()
        {
            if(agent.hasPath && agent.isStopped == false && agent.path.corners.Length > 0)
            {
                LoookAtTarget(agent.steeringTarget);
            }
        }

        public  Quaternion LoookAtTarget(Vector3 target, bool isSmooth = true)
        {
            Vector3 direction = target - _entity.transform.position;
            direction.y = 0;

            Quaternion LookRotation= Quaternion.LookRotation(direction);

            if(isSmooth)
            {
                _entity.transform.rotation = Quaternion.Lerp(_entity.transform.rotation, LookRotation,
                    Time.deltaTime * RotateSpeed);
            }
            else
                _entity.transform.rotation = LookRotation;

            return LookRotation;
        }

        public void SetStop(bool isStop) => agent.isStopped = isStop;
        public void SetVelocity(Vector3 velocity) => agent.velocity = velocity; 
        public void SetSpeed(float speed) => agent.speed = speed;
        public void SetDestination(Vector3 destination) => agent.SetDestination(destination);
    }
    
}