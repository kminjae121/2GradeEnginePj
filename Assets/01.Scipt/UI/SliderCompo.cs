using Blade.Combat;
using UnityEngine;
using UnityEngine.UI;

namespace _01.Scipt.UI
{
    public class SliderCompo : MonoBehaviour
    {
        [SerializeField] protected EntityHealth _healthCompo;
        [field: SerializeField] public Slider _slider { get; set; }
    }
}