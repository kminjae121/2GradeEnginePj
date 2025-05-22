using Blade.Combat;
using Member.Kmj._01.Scipt.Entity.AttackCompo;
using UnityEngine;

public abstract class SkillCompo : MonoBehaviour
{

    public EntityAnimatorTrigger _triggerCompo;

    [SerializeField] protected LayerMask _whatIsEnemy;

    [SerializeField] protected StatSO _skillDamage;

    [SerializeField] protected EntityStat _stat;

    [SerializeField] protected Vector3 _skillSize;
    [SerializeField] protected float _circleSize;

    [SerializeField] protected Entity _entity;



    [field :SerializeField] public float skillCoolTime { get; set; }
    [field: SerializeField] public float currentcoolTime { get; private set; }

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
