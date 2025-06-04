using System.Collections.Generic;
using Blade.Combat;
using Blade.Core.StatSystem;
using Member.Kmj._01.Scipt.Entity.AttackCompo;
using UnityEngine;

public abstract class SkillCompo : MonoBehaviour
{

    public EntityAnimatorTrigger _triggerCompo;

    [SerializeField] protected LayerMask _whatIsEnemy;

    public int skillLevel { get; set; } = 1;
    [field: SerializeField] public Vector3 _skillSize { get; set; }
    [SerializeField] protected float _circleSize;

    [SerializeField] protected Entity _entity;
    
    [field :SerializeField] public float skillCoolTime { get; set; }
    [field: SerializeField] public float currentcoolTime { get; private set; }
    
    [field:  SerializeField] public List<string> skillEffectName { get; set; } 

    public int currentSkillEffectNameIdx { get; set; }

    public Lifesteal _stealCompo;
    

    public void SkillUpdate()
    {
        if(currentcoolTime >= skillCoolTime)
            currentcoolTime = skillCoolTime;


        if (currentcoolTime >= skillCoolTime)
            return;
        else
            currentcoolTime += Time.deltaTime;
    }

    public bool CanUseSkill(string name)
    {
        if (currentcoolTime >= skillCoolTime)
            return true;
        else
            return false;
    }

    public void CurrentTimeClear(string name)
    {
        currentcoolTime = 0;
    }
    protected virtual void Skill()
    {

    }
    public virtual void GetSkill()
    {

    }

    public virtual void EventDefault()
    {

    }

    public virtual void SkillFeedback()
    {

    }

}
