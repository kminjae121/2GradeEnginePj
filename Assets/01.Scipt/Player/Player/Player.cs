using Member.Kmj._01.Scipt.Entity.AttackCompo;
using UnityEngine;

namespace _01.Scipt.Player.Player
{
    public class Player : Entity
    {
        [field: SerializeField] public PlayerInputSO PlayerInput { get; private set; }

        [SerializeField] private StateDataSO[] stateDatas;

        public CharacterMovement _movement { get; private set; }

        public EntityAnimatorTrigger _triggerCompo { get; private set; }

        public PlayerAttackCompo _attackCompo { get; private set; }
    
        public EntitySkillCompo _skillCompo { get; private set; }
    
        public MouseBarrerSkill _barrerSkill { get; private set; }
    
        public float rollingVelocity = 12f;
        public bool _isSkilling { get;  set; }

        public bool isDoingFollow { get; set; }

        public bool isFollowingAttack { get; set; } = false;

        public bool isUsePowerAttack { get; set; } = false;

        [SerializeField] private LayerMask _whatIsEnemey;
    
    
    
        private EntityStateMachine _stateMachine;
        public bool isUseSheld { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            _stateMachine = new EntityStateMachine(this,stateDatas);
            _attackCompo = GetCompo<PlayerAttackCompo>();
            _skillCompo = GetCompo<EntitySkillCompo>();
            _movement = GetCompo<CharacterMovement>();
            _triggerCompo = GetCompo<EntityAnimatorTrigger>();
        
        }


        private void HandleRollingPressed()
        {
            ChangeState("ROLLING");
        }
    
   

        private void Start()
        {
            _stateMachine.ChangeState("IDLE");
        }

        private void Update()
        {
            _stateMachine.UpdateStateMachine();
        }


        public void ChangeState(string newStateName) => _stateMachine.ChangeState(newStateName);

        protected override void HandleHit()
        {
        
        }

        protected override void HandleDead()
        {
        
        }

        protected override void HandleStun()
        {
        
        }

        public void PlayerDie()
        {
            _isSkilling = true;
            ChangeState("DIE");
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (((1 << collision.gameObject.layer) & _whatIsEnemey) != 0 && isDoingFollow)
            {
                ChangeState("STRONGATTACK");
            }
        }
    }
}
