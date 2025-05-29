using System;
using System.Collections;
using _01.Scipt.Blade.Entities;
using GondrLib.Dependencies;
using GondrLib.ObjectPool.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace _01.Scipt.Enemy
{
    public class Spawn : MonoBehaviour
    {
    
        public Transform[] spawnPoints;
        private int _level = 0;
        private int _killCount = 0;
    
        [SerializeField] private PoolingItemSO _enemyItem;
        [SerializeField] private TextMeshProUGUI _timetxt;
        [Inject]  private PoolManagerMono _poolManager;
        [SerializeField] private int _currentTime;
        private bool _isTimer;
        
        private float _startTime;
        public float _countdownDuration; 
        
        private PollingEnemy _enemy;


        private void Update()
        {
            if (GameManager.instance.killCount == _killCount)
            {
                GameManager.instance.killCount = 0;
                _killCount += 3;
                _level++;
                _isTimer = true;
                _startTime = Time.time; 
                print("턴 종료");
                StartCoroutine(SpawnTime());
            }

            if (_isTimer)
            {
                float timeLeft = _countdownDuration - (Time.time - _startTime);

                if (timeLeft <= 0)
                {
                    _timetxt.text = "";
                    timeLeft = 0;
                    _isTimer = false;
                }

                _timetxt.text = $"남은 시간 : {(int)timeLeft}";
            }
            else
            {
                _timetxt.text = "";
            }
        }

        private IEnumerator SpawnTime()
        {
            yield return new WaitForSeconds(_countdownDuration);
            _isTimer = false;
            SpawnEnemy();
        }

        private void SpawnEnemy()
        {
            int a = 0;
            
            for (int i = 0; i < _killCount; ++i)
            {
                _enemy = _poolManager.Pop<PollingEnemy>(_enemyItem);
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

