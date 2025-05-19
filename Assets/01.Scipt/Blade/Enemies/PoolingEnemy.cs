using _01.Scipt.Effect;
using GondrLib.ObjectPool.Runtime;
using UnityEngine;

public class PoolingEnemy : MonoBehaviour,IPoolable
{
    [field: SerializeField ] public PoolingItemSO PoolingType { get; private set; }
    public GameObject GameObject => gameObject;

    private Pool _myPool;
    [SerializeField] private GameObject poolEnemy;
    public string PoolingName { get; }
    private void Awake()
    {
            
    }

    public void SetUpPool(Pool pool)
    {
        _myPool = pool;
    }

        

    public void ResetItem()
    {
            
    }

    private void OnValidate()
    { 
        Debug.Assert(poolEnemy != null , "Enemy is not There");
    }
    
}
