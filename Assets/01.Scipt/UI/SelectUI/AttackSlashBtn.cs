using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AttackSlashBtn : MonoBehaviour
{
    [SerializeField] private PlayerAttackCompo _atkCompo;

    private int _currentAtkCnt = 0;
    [SerializeField] private int thisIdx = 0;

    public void UpSkillLevel()
    {
        if (_currentAtkCnt == 3)
        {
            LevelSystem.instance.itemList.RemoveAt(thisIdx);
            gameObject.SetActive(false);
            return;
        }
        _atkCompo.slashPercent += 20;
        _currentAtkCnt++;
    }
}