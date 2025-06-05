using System;
using _01.Scipt.UI;
using Blade.Combat;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpSlider : SliderCompo
{
    [SerializeField] private EntityHealth _health;

    private void Start()
    {
        _slider.maxValue = _health.maxHealth;
        _slider.value = _health.currentHealth;
    }

    private void Update()
    {
        
    }
}
