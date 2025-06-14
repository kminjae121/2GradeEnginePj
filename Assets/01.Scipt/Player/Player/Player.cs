using GondrLib.Dependencies;
using Member.Kmj._01.Scipt.Entity.AttackCompo;
using UnityEngine;
using UnityEngine.Serialization;

namespace _01.Scipt.Player.Player
{
    public class Player : Entity, IDependencyProvider
    {
        [field: SerializeField] public PlayerInputSO PlayerInput { get; private set; }

        [SerializeField] private StateDataSO[] stateDatas;
        [field: SerializeField] public GameObject FailUI {get; private set;}
 
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
    
        [Provide]
        public Player ProvidePlayer() => this;
    
        private EntityStateMachine _stateMachine;

        public Collider _collider { get; private set; }
        public bool isUseSheld { get; private set; }

        [field: SerializeField] public Transform _camPos { get; private set; } 

        protected override void Awake()
        {
            base.Awake();
            _stateMachine = new EntityStateMachine(this,stateDatas);
            _attackCompo = GetCompo<PlayerAttackCompo>();
            _skillCompo = GetCompo<EntitySkillCompo>();
            _movement = GetCompo<CharacterMovement>();
            _triggerCompo = GetCompo<EntityAnimatorTrigger>();
            _collider = GetComponent<Collider>();
            OnDead.AddListener(PlayerDie);
            OnHit.AddListener(HandleHit);
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
            PlayerComboSystem.Instance.ReduceCombo(15);
            AudioManager.Instance.PlaySFX("HitSound");
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
        
    }
}
