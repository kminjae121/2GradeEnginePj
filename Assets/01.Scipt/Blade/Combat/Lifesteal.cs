using Blade.Core.StatSystem;
using UnityEngine;

public class Lifesteal : MonoBehaviour
{
    [SerializeField] private EntityStat targetCompo;
    [SerializeField] private StatSO targetStat;
    [SerializeField] private PlayerAttackCompo _attackCompo;

    public void UpGradeStat()
    {
        targetCompo.AddModifier(targetStat, this, _attackCompo.bloodHp);
    }
}
