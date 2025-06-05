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
        [SerializeField] private TextMeshProUGUI _timetxt;
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
                if (_level == _endLevel)
                {
                    GameManager.instance.gameTime = 0;
            
                    if (FindObjectsOfType<EnemySkeletonSlave>().Length == 0)
                    {
                        Time.timeScale = 0;
                        string currentSceneName = SceneManager.GetActiveScene().name;
                        StageManager.Instance.ClearStage(currentSceneName);
                        _clearUI.SetActive(true);
                        Cursor.visible = true;         
                        Cursor.lockState = CursorLockMode.None;   
                    }
                }
                else
                {
                    GameManager.instance.gameTime = 0;
                    _currentTime += 5.3f;
                    _killCount += 1.3f;
                    _level++;
                    _isTimer = true;
                    _startTime = Time.time; 
                    _timetxt.text = $"현재 남은 라운드 : {_endLevel - _level}";
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
                int rand = Random.Range(1, 5);
                
                if (rand == 1)
                {
                    _enemy = _poolManager.Pop<EnemySkeletonSlave>(_enemyItem);
                }
                else if (rand == 2)
                {
                    _enemy = _poolManager.Pop<EnemySkeletonSlave>(_enemy2Item);
                }
                else if (rand == 3)
                {
                    _enemy = _poolManager.Pop<EnemySkeletonSlave>(_enemy3Item);
                }
                else if (rand == 4)
                {
                    _enemy = _poolManager.Pop<EnemySkeletonSlave>(_longRangeEnemy);
                }
                
                _enemy.GetCompo<EntityHealth>().Initialize(_enemy);
                _enemy.GetCompo<EntityHealth>().AfterInit();
                _enemy.Start();
                _enemy.transform.position = spawnPoints[a].position;

                ++a;
                if (a >= spawnPoints.Length)
                {
                    a = 0;
                }
            }
        }
    }
}

