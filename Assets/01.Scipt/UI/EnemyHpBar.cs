using System;
using Blade.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace _01.Scipt.UI
{
    public class EnemyHpBar : MonoBehaviour
    {
        [SerializeField] private EntityHealth _healthCompo;
        [SerializeField] private Slider _slider;
        [SerializeField] private EntityFinderSO _playerFinder;

        private void Start()
        {
            _slider.maxValue = _healthCompo.maxHealth;
        }

        private void Update()
        {
            _slider.transform.LookAt(_playerFinder.Target.transform.position);
            
            _slider.value = _healthCompo.currentHealth;
        }
    }
}