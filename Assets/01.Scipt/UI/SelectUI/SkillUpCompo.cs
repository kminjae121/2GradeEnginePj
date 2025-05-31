    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class SkillUpCompo : MonoBehaviour
    {
        [SerializeField] private SkillSO _skillSO;
        [SerializeField] private EntitySkillCompo _skillCompo;
        [SerializeField] private string skillCompoName;
        private SkillCompo _skill;
        [SerializeField] private int _countIdx;
        [SerializeField] private List<Vector3> _skillRange;
        private int _currentSkill;

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
                if (_currentSkill >= _skill.skillEffectName.Count)
                {
                    _skill._skillSize = _skillRange[_currentSkill];
                    LevelSystem.instance.itemList.RemoveAt(_countIdx);
                    gameObject.SetActive(false);
                }
            }
            else
            {
                _skillCompo.AddSkill(_skillSO);
                _skill._skillSize = _skillRange[_currentSkill];
                _skillSO = null;
            }
            
        }
    }
