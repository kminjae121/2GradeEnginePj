using UnityEngine;
using GondrLib.ObjectPool.Runtime;

public class EnemyDieCompo : MonoBehaviour
{
    public static EnemyDieCompo Instance { get; private set; }

    [SerializeField] private PoolManagerMono _poolManager;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void PushEnemy(IPoolable enemy)
    {
        if (_poolManager == null)
        {
            Debug.LogError("[EnemyDieCompo] PoolManager is not assigned!");
            return;
        }

        if (enemy == null)
        {
            Debug.LogError("[EnemyDieCompo] Tried to push a null enemy.");
            return;
        }

        _poolManager.Push(enemy);
    }
}