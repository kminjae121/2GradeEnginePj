 using System;
 using System.Collections.Generic;
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

    public float expValue;
    
    public float GetBallPercent { get; set; }
    [SerializeField] private TextMeshProUGUI _timeTxt;
    [SerializeField] private LevelSystem levelSystem;
    [SerializeField] private Slider _slider;

    [SerializeField] private TextMeshProUGUI _maxLevelTxt;
    [SerializeField] private TextMeshProUGUI _maxComboTxt;
    [SerializeField] private TextMeshProUGUI _maxTimeTxt;
    [SerializeField] private TextMeshProUGUI _currentComboTxt;
    public bool isEnd { get; set; } = false;
    private void Awake()
    {
        instance = this;
       _slider.maxValue = nextLevel;
       _levelTxt.text = $"Level : {level}";
       _currentExptxt.text = $"경험치 : {exp} : {nextLevel}";
    }

    private void Start()
    {
        BaseStatLibrary.instance.baseTxt.GetValueOrDefault("HealthBallProbabilty").text = $"회복구 확률 {GetBallPercent}%";
      //  levelSystem.Show();
    }
    
       private void Update()
       {
           gameTime += Time.deltaTime;
           endTime += (int)Time.deltaTime;
    
           expValue = Mathf.Lerp(expValue, exp,  10 * Time.deltaTime);
           _currentComboTxt.text = $"{PlayerComboSystem.Instance.CURRENTComboStr}";
           _currentComboTxt.color = PlayerComboSystem.Instance._txtColor;
           _maxLevelTxt.text = $"최대레벨 : {level}";
           _maxComboTxt.text = $"최대 콤보레벨 : {PlayerComboSystem.Instance.maxComboStr}";
           _maxTimeTxt.text = $"최대시간 : {(int)GameTimeManager.Instance.maxTime}";

           _slider.value = expValue;
           if (isEnd == false)
           {
                GameTimeManager.Instance.TickTime();
                _timeTxt.text = $"{(int)GameTimeManager.Instance.Gametime}초";
           }
       }
    
       public void GetExp()
       {
           exp += 1;
           _currentExptxt.text = $"경험치 : {exp} : {nextLevel}";

           if (exp >= nextLevel)
           {
               level++;
               exp = 0;
               nextLevel += 6;
               _slider.maxValue = nextLevel;
               _levelTxt.text = $"Level : {level}";
               _currentExptxt.text = $"경험치 : {exp} : {nextLevel}";
               levelSystem.Show();
           }
       }


      public void SpawnHpBall(Vector3 transform, Quaternion rotation)
      {
          Instantiate(_ballPrefab,transform,rotation);
      }
}
