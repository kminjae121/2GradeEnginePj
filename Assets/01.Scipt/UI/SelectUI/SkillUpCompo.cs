    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.UI;

    public class SkillUpCompo : MonoBehaviour
    {
        [SerializeField] private SkillSO _skillSO;
        [SerializeField] private EntitySkillCompo _skillCompo;
        [SerializeField] private string skillCompoName;
        private SkillCompo _skill;
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
                _skill = components[0] as SkillCompo;
            }


            print(_skill);
        }
        

        public void UpSkillLevel()
        {
            
            if (_skillSO == null)
            {
                _skill.skillLevel++;
                _skill.currentSkillEffectNameIdx++;
                _currentSkill++;
                _skill._skillSize = _skillRange[_currentSkill];
                
                if (_currentSkill >= 2)
                {
                    LevelSystem.instance.itemList.RemoveAt(_countIdx);
                    gameObject.SetActive(false);
                }
            }
            else
            {
                _skillCompo.AddSkill(_skillSO);
                Color color = _skillimage.color;
                color.a = Mathf.Clamp01(1);
                _skillimage.color = color;
                
                _skill._skillSize = _skillRange[_currentSkill];
                _skillSO = null;
            }
            
        }
    }
