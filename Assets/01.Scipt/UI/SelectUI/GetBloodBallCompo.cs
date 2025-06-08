using System;
using System.Collections.Generic;
using UnityEngine;

public class GetBloodBallCompo : MonoBehaviour
{
    private int _currentAtkCnt = 0;
    [SerializeField] private int thisIdx;

    private void Awake()
    {
       
    }

    public void UpSkillLevel()
    {
        if (_currentAtkCnt == 4)
        {
            LevelSystem.instance.itemList.RemoveAt(thisIdx);
            gameObject.SetActive(false);
            return;
        }
        GameManager.instance.GetBallPercent += 20;
        BaseStatLibrary.instance.baseTxt.GetValueOrDefault("HealthBallProbabilty").text = $"회복구 확률 {GameManager.instance.GetBallPercent}%";
        _currentAtkCnt++;
    }
}
