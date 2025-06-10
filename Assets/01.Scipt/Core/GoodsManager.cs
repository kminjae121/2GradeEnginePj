using UnityEngine;

namespace _01.Scipt.Core
{
    public class GoodsManager : MonoSingleton<GoodsManager>
    {

        public int bloodCoin { get; set; } = 10;

        public void UseCoin(int coin)
        {
            bloodCoin -= coin;
        }
        public void GetCoin(int coin)
        {
            bloodCoin += coin;
        }
    }
}