using System;
using _01.Scipt.Core;
using TMPro;
using UnityEngine;

public class CoinTxt : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI _coinTxt;

   private void Awake()
   {
      _coinTxt.text = $"현재 코인 : {GoodsManager.Instance.bloodCoin.BaseValue}";
   }

   public void UseCoin()
   {
      _coinTxt.text = $"현재 코인 : {GoodsManager.Instance.bloodCoin.BaseValue}";
   }
}
