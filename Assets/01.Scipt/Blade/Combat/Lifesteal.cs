using Blade.Combat;
using Blade.Core.StatSystem;
using UnityEngine;

public class Lifesteal : MonoBehaviour
{
    [SerializeField] private EntityStat targetCompo;
    [SerializeField] private StatSO targetStat;
    [SerializeField] private PlayerAttackCompo _attackCompo;
    [SerializeField] private EntityHealth _health;

    public void UpGradeStat()
    {
        print("됨");
        _health.HealHp(_attackCompo.bloodHp);
       // targetCompo.AddModifier(targetStat, this, _attackCompo.bloodHp);
    }

    public void MinusHealth()
    {
        _health.currentHealth -= 2.3f;
    }
}
