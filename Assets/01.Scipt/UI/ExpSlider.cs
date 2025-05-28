using System;
using UnityEngine;
using UnityEngine.UI;

public class ExpSlider : MonoBehaviour
{
    [SerializeField] private GameManager _exp;
    [SerializeField] private Slider _expSlider;
    

    private void Update()
    {
        _expSlider.value = _exp.exp;
    }
}
