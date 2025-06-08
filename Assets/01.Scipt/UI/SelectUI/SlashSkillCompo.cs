using System;
using System.Collections.Generic;
using _01.Scipt.Player.Skill;
using UnityEngine;
using UnityEngine.UI;

namespace _01.Scipt.UI.SelectUI
{
    public class SlashSkillCompo  : MonoBehaviour
    {
        [SerializeField] private SkillSO _skillSO;
        [SerializeField] private EntitySkillCompo _skillCompo;
        [SerializeField] private string skillCompoName;
        private SlashSkill _skill;
        [SerializeField] private int _countIdx;
        [SerializeField] private List<Vector3> _skillRange;
        private int _currentSkill = 0;
        [SerializeField] private Image _skillimage;
        private void Awake()
        {
            var type = Type.GetType(skillCompoName);
            
            var components = _skillCompo.GetComponentsInChildren(type, true);

            if (components.Length > 0)
            {
                _skill = components[0] as SlashSkill;
            }
            
            
            Color color = _skillimage.color;
            color.a = Mathf.Clamp01(1);
        }

        private void Update()
        {
            int count = transform.GetSiblingIndex();
            _countIdx = count;
        }

        public void UpSkillLevel()
        {
                _skill.skillLevel++;
                _skill.currentEffectNum++;
                _currentSkill++;
                
                if (_currentSkill >= 1)
                {
                    LevelSystem.instance.itemList.RemoveAt(_countIdx);
                    gameObject.SetActive(false);
                }
            
        }
    }
}