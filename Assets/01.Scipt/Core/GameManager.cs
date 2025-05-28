using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float gameTime;
    public float maxGameTime;
    public int level{ get; set; }

    public int killCount { get; set; } = 0;

    public int endTime;

    public float exp { get; set; }

    public int currentGameTime { get; set; }


    public int nextLevel = 4;

    [SerializeField] private LevelSystem levelSystem;
    [SerializeField] private Slider _slider;
    private void Awake()
    {
        instance = this;
        SetStartValue();
        _slider.maxValue = nextLevel;
    }

    private void Start()
    {
        levelSystem.Show();
    }

    private void Update()
    {
        endTime += (int)Time.deltaTime;

        _slider.value = exp;
    }

    public void GetExp()
    {
        exp += 1;
        
        if (exp >= nextLevel)
        {
            level++;
            exp = 0;
            nextLevel += 12;
            _slider.maxValue = nextLevel;
            print("래밸업됨");
            levelSystem.Show();
        }
    }

    public void SetStartValue()
    {
        exp = 0;
        nextLevel = 3;
        level = 0;
    }
}
