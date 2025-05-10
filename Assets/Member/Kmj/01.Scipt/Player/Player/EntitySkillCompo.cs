using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Member.Kmj._01.Scipt.Entity.AttackCompo;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class EntitySkillCompo : MonoBehaviour, IEntityComponet
{
    [SerializeField] private List<SkillSO> _skillList;


    public Dictionary<string, SkillCompo> SkillList;

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
    }
}
