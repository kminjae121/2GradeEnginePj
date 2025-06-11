using System;
using System.Collections.Generic;
using _01.Scipt.Core;
using Blade.Core.StatSystem;
using TMPro;
using UnityEngine;

public class StoreItem : MonoBehaviour
{
    [SerializeField] private List<StatSO> _stats;
    public static float price { get; private set; }
    [SerializeField] private List<int> upgradeStat;
    [SerializeField] private CoinTxt _coinTxt;

    [SerializeField] private TextMeshProUGUI _priceTmp;
    private void Awake()
    {
        price = 65;
        _priceTmp.text = $"가격 : {price}";   
    }

    public void AddAttackDamage()
    {
        if (GoodsManager.Instance.bloodCoin.BaseValue <= 0)
            return;
        if (GoodsManager.Instance.bloodCoin.BaseValue - price < 0)
            return;
        GoodsManager.Instance.UseCoin((int)price);
        _coinTxt.UseCoin();
        price *= 1.4f;
        _stats[0].BaseValue += upgradeStat[0];
        _priceTmp.text = $"가격 : {(int)price}";
    }

    public void AddSkilDamage()
    {
        if (GoodsManager.Instance.bloodCoin.BaseValue <= 0)
            return;
        if (GoodsManager.Instance.bloodCoin.BaseValue - price < 0)
            return;
        GoodsManager.Instance.UseCoin((int)price);
        _coinTxt.UseCoin();
        price *= 1.4f;
        _stats[1].BaseValue += upgradeStat[1];
        _priceTmp.text = $"가격 : {(int)price}";
    }

    public void AddBloodEat()
    {
        if (GoodsManager.Instance.bloodCoin.BaseValue <= 0)
            return;
        if (GoodsManager.Instance.bloodCoin.BaseValue - price < 0)
            return;
        GoodsManager.Instance.UseCoin((int)price);
        _coinTxt.UseCoin();
        price *= 1.4f;
        _stats[2].BaseValue += upgradeStat[2];
        _priceTmp.text = $"가격 : {(int)price}";
    }
}
