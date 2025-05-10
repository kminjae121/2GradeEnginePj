using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [field: SerializeField] public PoolManager pool;
    public float gameTime;
    public float maxGameTime;
    public int level{ get; set; }

    public int killCount{ get; set; }

    public int exp { get; set; }

    public int currentGameTime { get; set; }


    public int nextLevel = 3;

    [SerializeField] private LevelSystem levelSystem;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        currentGameTime +=  (int)Time.deltaTime;

        if (gameTime > maxGameTime)
            gameTime = maxGameTime;
    }

    public void GetExp()
    {
        exp++;

        if (exp >= nextLevel)
        {
            level++;
            exp = 0;
            nextLevel += 12;
            levelSystem.Show();
        }
    }
}
