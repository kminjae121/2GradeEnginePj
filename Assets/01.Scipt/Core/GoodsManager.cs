using UnityEngine;

namespace _01.Scipt.Core
{
    public class GoodsManager : MonoBehaviour
    {
        public static GoodsManager instance;
        
        public int bloodCoin { get; set; }

        public void UseCoin(int coin)
        {
            bloodCoin = coin;
        }
        public void GetCoin(int coin)
        {
            bloodCoin += coin;
        }
    }
}