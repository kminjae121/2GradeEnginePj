using UnityEngine;
using UnityEngine.UI;


public enum SkillType
{
    Attack,Defense,
}
[CreateAssetMenu(menuName = "Skill/So", fileName = "SKills")]
public class SkillSO : ScriptableObject
{
    public string skillName;
    public Image skillUIImage;
    public string className;

    public SkillType skillType;
}
