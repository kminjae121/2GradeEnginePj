 using System;
using TMPro;
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
    
    [SerializeField] private TextMeshProUGUI _levelTxt;
    [SerializeField] private TextMeshProUGUI _currentExptxt;
    [SerializeField] private GameObject _ballPrefab;

    public int nextLevel = 4;

    public float GetBallPercent { get; set; }
    [SerializeField] private LevelSystem levelSystem;
    [SerializeField] private Slider _slider;
    private void Awake()
    {
        instance = this;
       SetStartValue();
       _slider.maxValue = nextLevel;
       _levelTxt.text = $"현재 레벨 : {level}";
       _currentExptxt.text = $"경험치 : {exp} : {nextLevel}";
    }

    private void Start()
    {
      //  levelSystem.Show();
    }
    
       private void Update()
       {
           gameTime += Time.deltaTime;
           endTime += (int)Time.deltaTime;
    
           _slider.value = exp;
       }
    
       public void GetExp()
       {
           exp += 1;
           _currentExptxt.text = $"경험치 : {exp} : {nextLevel}";

           if (exp >= nextLevel)
           {
               level++;
               exp = 0;
               nextLevel += 1;
               _slider.maxValue = nextLevel;
               _levelTxt.text = $"현재 레벨:{level}";
               _currentExptxt.text = $"경험치 : {exp} : {nextLevel}";
               levelSystem.Show();
           }
       }
    
      public void SetStartValue()
      {
          exp = 0;
          nextLevel = 1;
          level = 0;
      }


      public void SpawnHpBall(Vector3 transform, Quaternion rotation)
      {
          Instantiate(_ballPrefab,transform,rotation);
      }
}
