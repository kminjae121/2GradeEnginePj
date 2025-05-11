using UnityEngine;

public interface IPoolable
{
    public string PoolingName { get; }
    public GameObject GetGameObject();
    public void ResetItem();
}
