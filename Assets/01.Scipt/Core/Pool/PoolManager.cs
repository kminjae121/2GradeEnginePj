using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoSingleton<PoolManager>
{
    [SerializeField] private PoolingListSO _poolList;

    private Dictionary<string, Pool> _pools;

    protected override void Awake()
    {
        base.Awake();
        _pools = new Dictionary<string, Pool>();

        foreach (PoolItemSO item in _poolList.list)
        {
            CreatePool(item.prefab, item.count);
        }
    }

    private void CreatePool(GameObject prefab, int count)
    {
        IPoolable poolable = prefab.GetComponent<IPoolable>();
        if (poolable == null)
        {
            Debug.LogWarning($"Gameobject {prefab.name} has not Ipoolable script");
            return;
        }


        Pool pool = new Pool(poolable,transform, count);
        _pools.Add(poolable.PoolingName, pool);
    }

    public IPoolable Pop(string itemName)
    {
        if (_pools.ContainsKey(itemName))
        {
            IPoolable item = _pools[itemName].Pop();

            item.ResetItem();
            return item;
        }
        Debug.LogError($"There is no pool {itemName}");
        return null;
    }

    public void Push(IPoolable item)
    {
        if (_pools.ContainsKey(item.PoolingName))
        {
            _pools[item.PoolingName].Push(item);
            return;
        }
        Debug.LogError($"There is no pool {item.PoolingName}");
    }
}
