using Member.Kmj._01.Scipt.Entity.AttackCompo;
using UnityEngine;

public class DamageCaster : MonoBehaviour
{
    private Entity _owner;
    [SerializeField] private LayerMask _whatIsEnemy;
    public void InitCaster(Entity owner)
    {
        _owner = owner;
    }

    public bool CastDamage(float damage)
    {
        RaycastHit hit;
        bool isHit = Physics.Raycast(transform.position,transform.forward, out hit,4,_whatIsEnemy);

        Debug.Log(isHit);


        if (isHit)
        {
            Debug.Log(hit.transform.name);
            hit.transform.GetComponentInChildren<IDamgable>().ApplyDamage(damage, false,0, _owner);
        }

        return isHit;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,transform.lossyScale.x * 0.5f);
        Gizmos.color = Color.white;
    }
}
