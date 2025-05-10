using UnityEngine;

[CreateAssetMenu(fileName = "AttackData", menuName = "SO/Combat/AttackData", order = 0)]
public class AttackDataSO : ScriptableObject
{
    public string attackName;
    public float movementPower;
    public float damageMulitiplier = 1f;
    public float damageIncrease = 0;
    public bool isPowerAttack;

    private void OnEnable()
    {
        attackName = this.name;
    }
}
