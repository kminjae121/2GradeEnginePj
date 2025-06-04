using System;
using Blade.Combat;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;


    [SerializeField] private EntityHealth _health;

    private void Start()
    {
        _slider.maxValue = _health.maxHealth;
    }

    private void Update()
    {
        _slider.value = _health.currentHealth;
    }
}
