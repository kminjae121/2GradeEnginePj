using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawn : MonoBehaviour
{
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
        GameObject enemy = GameManager.instance.pool.Get(level);
        enemy.transform.position = spawnPoints[Random.Range(1, spawnPoints.Length)].position;
    }
}
