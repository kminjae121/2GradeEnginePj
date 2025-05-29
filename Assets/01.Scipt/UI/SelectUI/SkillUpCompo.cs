using System;
using System.Linq;
using UnityEngine;

public class SkillUpCompo : MonoBehaviour
{
    [SerializeField] private SkillSO[] _skillSO;
    [SerializeField] private EntitySkillCompo _skillCompo;
    [SerializeField] private int _countIdx;
    private int _currentSkill;

    private void Update()
    {
    }

    public void UpSkillLevel()
    {
        _skillCompo.AddSkill(_skillSO[_currentSkill]);
        _currentSkill++;    
        
        if (_currentSkill >= _skillSO.Length)
        {
            LevelSystem.instance.itemList.RemoveAt(_countIdx);
            gameObject.SetActive(false);
        }
    }
}
