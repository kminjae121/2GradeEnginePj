using System;
using System.Collections.Generic;
using System.Linq;
using Blade.Combat;
using UnityEngine;
using UnityEngine.Events;

namespace Member.Kmj._01.Scipt.Entity.AttackCompo
{
    public abstract class Entity : MonoBehaviour
    {

        public UnityEvent OnHit;
        public UnityEvent OnDead;
        public UnityEvent OnStun;

        public bool IsDead { get; set; }
        public int DeadBodyLayer { get; private set; }

        protected Dictionary<Type, IEntityComponet> _componets;

        protected virtual void Awake()
        {
            _componets = new Dictionary<Type, IEntityComponet>();
            AddComponets();
            InitializeComponts();
            AfterInitialize();
        }

        public virtual void AddComponets()
        {
            GetComponentsInChildren<IEntityComponet>().ToList().
                ForEach(comp => _componets.Add(comp.GetType(), comp));
        }
        

        protected virtual void AfterInitialize()
        {
            _componets.Values.OfType<IAfterInit>().ToList().ForEach(compo => compo.AfterInit());
            OnHit.AddListener(HandleHit);
            OnDead.AddListener(HandleDead);
            OnStun.AddListener(HandleStun);
        }

        protected virtual void OnDestroy()
        {
            OnHit.RemoveListener(HandleHit);
            OnDead.RemoveListener(HandleDead);
            OnStun.RemoveListener(HandleStun);
        }
        
        public IEntityComponet Getcompo(Type type)
            => _componets.GetValueOrDefault(type);

        private void InitializeComponts()
        {
            _componets.Values.ToList().ForEach(comp => comp.Initialize(this));
        }

        public T GetCompo<T>() where T : IEntityComponet
            => (T)_componets.GetValueOrDefault(typeof(T));
        
        public IEntityComponet GetCompo(Type type)
            => _componets.GetValueOrDefault(type);

        protected abstract void HandleHit();
        protected abstract void HandleDead();
        protected abstract void HandleStun();
        

    }
}
