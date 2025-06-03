using System.Collections.Generic;
using _01.Scipt.Core;
using Blade.Core.StatSystem;
using TMPro;
using UnityEngine;

public class StoreItem : MonoBehaviour
{
    [SerializeField] private List<StatSO> _stats;
    [field: SerializeField] public float price { get; private set; }
    [SerializeField] private List<int> upgradeStat;
    [SerializeField] private CoinTxt _coinTxt;
    public void AddAttackDamage()
    {
        GoodsManager.Instance.UseCoin((int)price);
        _coinTxt.UseCoin();
        price *= 1.4f;
        _stats[0].BaseValue += upgradeStat[0];
    }

    public void AddSkilDamage()
    {
        GoodsManager.Instance.UseCoin((int)price);
        _coinTxt.UseCoin();
        price *= 1.4f;
        _stats[1].BaseValue += upgradeStat[1];
    }

    public void AddBloodEat()
    {
        GoodsManager.Instance.UseCoin((int)price);
        _coinTxt.UseCoin();
        price *= 1.4f;
        _stats[2].BaseValue += upgradeStat[2];
    }
}
