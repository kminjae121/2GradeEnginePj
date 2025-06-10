using System;
using System.Collections;
using System.Collections.Generic;
using _01.Scipt.Player.Player;
using Member.Kmj._01.Scipt.Entity.AttackCompo;
using UnityEngine;
using UnityEngine.UI;

public enum MaxComboLevel
{
    D,C,B,A,S
}
public class PlayerComboSystem : MonoSingleton<PlayerComboSystem>, IEntityComponet
{
    public float _CurrentComboStat { get; set; }

    public string maxComboStr { get; set; } = "D";

    private MaxComboLevel _comboLevel;
    public string maxRecordComboStr { get; set; } = "D";
    public string CURRENTComboStr { get; set; } = "D";
    
    
    private Coroutine _ComboCoroutine;

    public float comboStat;
    
    public float reduceAmount { get; set; } = 10f;
    
    public bool isInRange { get; set; }

    public Color _txtColor;

    private Player _player;
    private void Awake()
    {
        _CurrentComboStat = 0;

        _comboLevel = MaxComboLevel.D;
    }
    public void Initialize(Entity entity)
    {
        _player = entity as Player;
    }
    

    private void Update()
    {
        print(maxComboStr);
        comboStat = Mathf.Lerp(comboStat, _CurrentComboStat, Time.deltaTime);
    }

    public void RaiseCombo(float amount)
    {
        if (isInRange) return; 

        _CurrentComboStat += amount;
        switch (comboStat)
        {
            case <= 100:
                CURRENTComboStr = "D";
                if (_comboLevel < MaxComboLevel.C)
                {
                    _comboLevel = MaxComboLevel.D;
                    maxComboStr = "D";
                    maxRecordComboStr = "D";
                    _txtColor = Color.red;
                }
                break;
            case <= 150:
                CURRENTComboStr = "C";
                if (_comboLevel < MaxComboLevel.B)
                {
                    _comboLevel = MaxComboLevel.C;
                    maxComboStr = "C";
                    maxRecordComboStr = "C";
                    _txtColor = Color.blue;
                }

                break;
            case <= 250:
                CURRENTComboStr = "B";
                if (_comboLevel < MaxComboLevel.A)
                {
                    _comboLevel = MaxComboLevel.B;
                    maxComboStr = "B";
                    maxRecordComboStr = "B";
                    _txtColor = Color.green;
                }

                break;
            case <= 340:
                CURRENTComboStr = "A";
                if (_comboLevel < MaxComboLevel.S)
                {
                    _comboLevel = MaxComboLevel.A;
                    maxComboStr = "A";
                    maxRecordComboStr = "A";
                    _txtColor = Color.magenta;
                }

                break;
            case <= 700:
                CURRENTComboStr = "S";
                maxComboStr = "S";
                maxRecordComboStr = "S";
                _txtColor = Color.yellow;
                break;
        }
        
        if (_ComboCoroutine != null)
            StopCoroutine(_ComboCoroutine); 
        
        _ComboCoroutine = StartCoroutine(ReduceFury());
    }

    private IEnumerator ReduceFury()
    {
        yield return new WaitForSeconds(1.5f); 

        while (_CurrentComboStat > 0)
        {
            _CurrentComboStat -= reduceAmount;
            yield return new WaitForSeconds(0.1f);
            
            switch (comboStat)
            {
                case <= 100:
                    CURRENTComboStr = "D";
                    _txtColor = Color.red;
                    break;
                case <= 150:
                    CURRENTComboStr = "C";
                     _txtColor = Color.blue;
                    break;
                case <= 250:
                    CURRENTComboStr = "B";
                    _txtColor = Color.green;
                    break;
                case <= 340:
                    CURRENTComboStr = "A";
                    _txtColor = Color.magenta;
                    break;
                case <= 700:
                    CURRENTComboStr = "S";
                    _txtColor = Color.yellow;
                    break;
            }
        }

        _ComboCoroutine = null;
    }

    public void ReduceCombo(float amount)
    {
        if (isInRange) return; 

        _CurrentComboStat -= amount;

        switch (comboStat)
        {
            case <= 100:
                CURRENTComboStr = "D";
                _txtColor = Color.red;
                break;
            case <= 150:
                CURRENTComboStr = "C";
                _txtColor = Color.blue;
                break;
            case <= 250:
                CURRENTComboStr = "B";
                _txtColor = Color.green;
                break;
            case <= 340:
                CURRENTComboStr = "A";
                _txtColor = Color.magenta;
                break;
            case <= 700:
                CURRENTComboStr = "S";
                _txtColor = Color.yellow;
                break;
        }
    }
    
    
    public bool IsInRage => isInRange;

}
