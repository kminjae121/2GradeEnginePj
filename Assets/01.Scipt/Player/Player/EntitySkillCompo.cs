using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using _01.Scipt.Player.Player;
using Blade.Core.StatSystem;
using Member.Kmj._01.Scipt.Entity.AttackCompo;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class EntitySkillCompo : MonoBehaviour, IEntityComponet, IAfterInit
{
    [SerializeField] private List<SkillSO> _skillList;

    public float skillDamage { get; set; }
    public float BaseskillDamage { get; private set; }
    
    [SerializeField] private StatSO _skilldamageSo;
    
    public Dictionary<string, SkillCompo> SkillList;

    private EntityStat _statCompo;
    private Player player;
    public virtual void Initialize(Entity entity)
    {
        player = entity as Player;
        SkillList = new Dictionary<string, SkillCompo>();

        if(SkillList == null)
            return;
        else
        {
            foreach (var skillSo in _skillList)
            {
                var type = Type.GetType(skillSo.className);

                if (type == null)
                    return;

                var components = entity.GetComponentsInChildren(type, true);

                if (components.Length > 0)
                {
                    SkillCompo component = components[0] as SkillCompo;

                   

                    SkillList.Add(skillSo.skillName, component);
                }
            }
        }
           

        if (SkillList == null)
            return;
        else
            SkillList.Values.ToList().ForEach(skill => skill.GetSkill());

        _statCompo = entity.GetCompo<EntityStat>();
    }

    private void Start()
    {
        BaseStatLibrary.instance.baseTxt.GetValueOrDefault("SkillDamage").text = $"스킬데미지 : {BaseskillDamage}";
    }

    public void AddSkill(SkillSO skillSO)
    {
        if (skillSO == null) return;
        _skillList.Add(skillSO);

        var type = Type.GetType(skillSO.className);

        var components = player.GetComponentsInChildren(type, true);

        if (components.Length > 0)
        {
            SkillCompo component = components[0] as SkillCompo;
             
            SkillList.Add(skillSO.skillName, component);
            SkillList.GetValueOrDefault(skillSO.skillName).GetSkill();
        }

    }


    private void Update()
    {
        if (SkillList == null)
            return;

        SkillList.Values.ToList().ForEach(skill => skill.SkillUpdate());
    }


    public void DefaltSkill()
    {
        if (SkillList == null)
            return;

        SkillList.Values.ToList().ForEach(skill => skill.EventDefault());
    }
    private void OnDestroy()
    {
        DefaltSkill();
        StatSO targetStat = _statCompo.GetStat(_skilldamageSo);
        Debug.Assert(targetStat != null, $"{_skilldamageSo.statName} stat could not be found");
        targetStat.OnValudeChanged -= HandleSkillChange;
    }
   
    private void HandleSkillChange(StatSO stat, float currentValue, float previousValue)
    {
        skillDamage += currentValue - previousValue;
        BaseskillDamage += currentValue - previousValue;
        
        BaseStatLibrary.instance.baseTxt.GetValueOrDefault("SkillDamage").text = $"스킬데미지 : {BaseskillDamage}";
    }
   
    public void AfterInit()
    {
        StatSO targetStat = _statCompo.GetStat(_skilldamageSo);
        Debug.Assert(targetStat != null, $"{_skilldamageSo.statName} stat could not be found");
        targetStat.OnValudeChanged += HandleSkillChange;
        skillDamage = targetStat.BaseValue;
        BaseskillDamage = targetStat.BaseValue;
    }
}
