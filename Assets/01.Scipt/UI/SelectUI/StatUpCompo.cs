using Blade.Core.StatSystem;
using UnityEngine;

public class StatUpCompo : MonoBehaviour
{
   
    [SerializeField] private EntityStat targetCompo;
    [SerializeField] private StatSO targetStat;
    [SerializeField] private float modifyValue;

    public void UpGradeStat()
    {
        targetCompo.AddModifier(targetStat, this, modifyValue);
    }
            
}
