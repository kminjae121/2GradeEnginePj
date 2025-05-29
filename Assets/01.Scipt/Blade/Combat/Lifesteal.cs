using Blade.Core.StatSystem;
using UnityEngine;

public class Lifesteal : MonoBehaviour
{
    [SerializeField] private EntityStat targetCompo;
    [SerializeField] private StatSO targetStat;
    [SerializeField] private PlayerAttackCompo _attackCompo;

    public void UpGradeStat()
    {
        print(_attackCompo.bloodHp);
        targetCompo.AddModifier(targetStat, this, _attackCompo.bloodHp);
    }
}
