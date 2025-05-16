using Blade.Combat;
using UnityEngine;

public class UpSkillDamageCaster : DamageCaster
{
    [SerializeField] private Vector3 boxSize;
    
    

    public override void CastDamage(Vector3 position, Vector3 direction, AttackDataSO attackData)
    {
        Vector3 startPos = position;

        var collider = Physics.OverlapBox(transform.position, boxSize,
            Quaternion.identity,whatIsEnemy);


        foreach (var Obj in collider)
            if (Obj.TryGetComponent(out IDamageable damage))
            {
                Debug.Log("Up공격됨");
                //   damage.ApplyDamage(atkDamage,Obj.transform.position,Obj.transform.forward,);
            }
            else
            {
                print("왔는데 없음");
            }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, boxSize);
        Gizmos.color = Color.white;
    }
        
#endif
}
