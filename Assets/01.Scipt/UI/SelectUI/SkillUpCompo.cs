using System.Linq;
using UnityEngine;

public class SkillUpCompo : MonoBehaviour
{
    [SerializeField] private SkillSO[] _skillSO;

    [SerializeField] private EntitySkillCompo _skillCompo;

    private int _currentSkill;

    public void UpSkillLevel()
    {
        if (_currentSkill >= _skillSO.Length - 1)
            LevelSystem.instance.itemList.ToList().Remove(this.gameObject);
        
        _skillCompo.AddSkill(_skillSO[_currentSkill]);
        _currentSkill++;    
    }
}
