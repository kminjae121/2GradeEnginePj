using System;
using System.Collections;
using _01.Scipt.Blade.Entities;
using Blade.Combat;
using Blade.Enemies.Skeletons;
using GondrLib.Dependencies;
using GondrLib.ObjectPool.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace _01.Scipt.Enemy
{
    public class Spawn : MonoBehaviour
    {
    
        public Transform[] spawnPoints;
        private int _level = 0;
        private float _killCount = 1;
        
        [SerializeField] private PoolingItemSO _enemyItem;
        [SerializeField] private PoolingItemSO _enemy2Item;
        [SerializeField] private PoolingItemSO _enemy3Item;
        [SerializeField] private PoolingItemSO _longRangeEnemy;
        [SerializeField] private GameObject _clearUI;
        [Inject]  private PoolManagerMono _poolManager;
        [SerializeField] private float _currentTime = 3;
        private bool _isTimer;
        
        private float _startTime;
        public float _countdownDuration; 
        
        private EnemySkeletonSlave _enemy;

        [SerializeField] private int _endLevel;


        private void Update()
        {
            if (GameManager.instance.gameTime > _currentTime)
            {
                {
                    FadeText.Instance.FindTxt("RoundTxt").gameObject.SetActive(true);
                    FadeText.Instance.FindTxt("RoundTxt").alpha = 255f;
                    GameManager.instance.gameTime = 0;
                    _currentTime = 30;
                    _killCount += 7f;
                    _level++;
                    _isTimer = true;
                    _startTime = Time.time;
                    FadeText.Instance.FindTxt("RoundTxt").text = $"Round{_level}";
                    FadeText.Instance.FadeTxt(0, 3, "RoundTxt");
                    StartCoroutine(SpawnTime());
                }
            }
        }

        private IEnumerator SpawnTime()
        {
            yield return new WaitForSeconds(0);
            SpawnEnemy();
        }

        private void SpawnEnemy()
        {
            int a = 0;

            for (int i = 0; i < (int)_killCount; ++i)
            {
                EnemySkeletonSlave enemy; // <- 지역변수로 바꿉니다.
                int rand = Random.Range(1, 4);

                if (rand == 1)
                {
                    enemy = _poolManager.Pop<EnemySkeletonSlave>(_enemyItem);
                }
                else if (rand == 2)
                {
                    enemy = _poolManager.Pop<EnemySkeletonSlave>(_enemy2Item);
                }
                else
                {
                    enemy = _poolManager.Pop<EnemySkeletonSlave>(_enemy3Item);
                }

                enemy.GetCompo<EntityHealth>().Initialize(enemy);
                enemy.GetCompo<EntityHealth>().AfterInit();
                enemy.Start();
                enemy.transform.GetChild(0).rotation = Quaternion.Euler(0, 0, 0);
                enemy.transform.position = spawnPoints[a].position;

                ++a;
                if (a >= spawnPoints.Length)
                {
                    a = 0;
                }
            }
            
            EnemySkeletonSlave longRangeEnemy = _poolManager.Pop<EnemySkeletonSlave>(_longRangeEnemy);
            longRangeEnemy.GetCompo<EntityHealth>().Initialize(longRangeEnemy);
            longRangeEnemy.GetCompo<EntityHealth>().AfterInit();
            longRangeEnemy.Start();
            longRangeEnemy.transform.GetChild(0).rotation = Quaternion.Euler(0, 0, 0);
            longRangeEnemy.transform.position = spawnPoints[a].position;
        }
    }
}

