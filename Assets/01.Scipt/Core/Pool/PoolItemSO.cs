using UnityEngine;

[CreateAssetMenu(menuName = "SO/Pool/Item")]
public class PoolItemSO : ScriptableObject
{
    public string poolName;
    public GameObject prefab;
    public int count;

    private void OnValidate()
    {
        if (prefab != null)
        {
            IPoolable item = prefab.GetComponent<IPoolable>();
            if (item == null)
            {
                Debug.LogWarning($"Cant find Poolable interface : check {prefab.name}");
                prefab = null;
            }
            else
            {
                poolName = item.PoolingName;
            }
        }
    }
}
