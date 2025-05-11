using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawn : MonoBehaviour
{
    [SerializeField] private string enemyPoolName = "Enemy";
    public Transform[] spawnPoints;

    private int level;
    private float timer;

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
        IPoolable enemy = PoolManager.Instance.Pop(enemyPoolName);

        if (enemy == null) return;
        
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject enemyObj = enemy.GetGameObject();
        enemyObj.transform.position = spawnPoint.position;
        enemyObj.transform.rotation = spawnPoint.rotation;
        enemyObj.SetActive(true);
    }
}
