using System;
using Blade.Combat;
using Blade.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace _01.Scipt.UI
{
    public class EnemyHpBar : SliderCompo
    {
        [SerializeField] private EntityFinderSO _playerFinder;

        private void Start()
        {
            _slider.maxValue = _healthCompo.maxHealth;
            _slider.value = _healthCompo.currentHealth;
        }

        private void Update()
        {
            _slider.transform.LookAt(_playerFinder.Target.transform.position);
        }
    }
}