using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingGame : MonoBehaviour
{
    public static LoadingGame Instance;
    [SerializeField] private Slider _slider;

    [field: SerializeField] public float loadingSpeed { get; set; }

    [SerializeField] private TextMeshProUGUI _loadingTxt;

    private float _progress = 0f;
    public bool IsEnd { get; set; } = false;

    private void Start()
    {
        Instance = this;
        IsEnd  = false;
        AudioManager.Instance.StopBGM();
        _slider.value = 0;
        Time.timeScale = 0;
    }

    private void Update()
    {
        _progress += 0.0012f * loadingSpeed;
        _progress = Mathf.Clamp01(_progress);
        
        _slider.value = Mathf.Lerp(_slider.value, _progress, 0.01f);
        
        _loadingTxt.text = $"{Mathf.RoundToInt(_slider.value * 100)}%";
        
        if (_slider.value >= 0.99f)
        {
            _slider.value = 1f;
            Time.timeScale = 1;
            IsEnd  = true;
            gameObject.SetActive(false);
            if (SceneManager.GetActiveScene().name == "Stage1")
            {
                AudioManager.Instance.PlayBGM("GameBGM");
            }
            else
            {
                AudioManager.Instance.PlayBGM("MainBGM");
            }
        }
    }
}
