using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SkillCooldownUI : MonoBehaviour
{
    [SerializeField] private Image skillFillImage; // 쿨타임 UI 이미지
    [SerializeField] private string _skillname;
    [SerializeField] private EntitySkillCompo _skillCompo;
    private SkillCompo _skill;


    private void Awake()
    {
        var type = Type.GetType(_skillname);
            
        var components = _skillCompo.GetComponentsInChildren(type, true);

        if (components.Length > 0)
        {
            _skill = components[0] as SkillCompo;
        }
    }

    private void Update()
    {
        skillFillImage.fillAmount = (_skill.currentcoolTime / _skill.skillCoolTime);
    }
}