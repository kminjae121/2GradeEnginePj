using System;
using Blade.Effects;
using Blade.Enemies;
using Blade.Entities;
using GondrLib.Dependencies;
using GondrLib.ObjectPool.Runtime;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawn : MonoBehaviour
{
    
    public Transform[] spawnPoints;
    private int level;
    private float timer;
    
    [SerializeField] private PoolingItemSO _enemyItem;
    [Inject]  private PoolManagerMono _poolManager;

    private Enemy _enemy;
    
    private void Awake()
    {
        spawnPoints = GetComponentsInChildren<Transform>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        timer += Time.deltaTime;

        level = Mathf.FloorToInt(GameManager.instance.gameTime / 10f);

        if (timer > (level == 0 ? 0.5f : 0.2f))
        {
            timer = 0;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        _enemy = _poolManager.Pop<Enemy>(_enemyItem);
        
       /* IPoolable enemy = PoolManager.Instance.Pop(enemyPoolName);

        if (enemy == null) return;
        
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject enemyObj = enemy.GetGameObject();
        enemyObj.transform.position = spawnPoint.position;
        enemyObj.transform.rotation = spawnPoint.rotation;
        enemyObj.SetActive(true);*/
       
    }
}

