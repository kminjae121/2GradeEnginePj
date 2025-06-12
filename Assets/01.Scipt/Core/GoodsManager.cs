using System;
using Blade.Core.StatSystem;
using UnityEngine;

namespace _01.Scipt.Core
{
    public class GoodsManager : MonoSingleton<GoodsManager>
    {

        public StatSO bloodCoin;
        

        private void Update()
        {
           
        }


        public void UseCoin(int coin)
        {
            bloodCoin.BaseValue -= coin;
        }
        public void GetCoin(int coin)
        {
            bloodCoin.BaseValue += coin;
        }
    }
}