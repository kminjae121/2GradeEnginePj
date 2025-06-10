using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingGame : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    [field: SerializeField] public float loadingSpeed { get; set; }

    [SerializeField] private TextMeshProUGUI _loadingTxt;

    private float _progress = 0f;

    private void Start()
    {
        AudioManager.Instance.StopBGM();
        _slider.value = 0;
        Time.timeScale = 1;
    }

    private void Update()
    {
        _progress += Time.deltaTime * loadingSpeed;
        _progress = Mathf.Clamp01(_progress);
        
        _slider.value = Mathf.Lerp(_slider.value, _progress, 0.01f);
        
        _loadingTxt.text = $"{Mathf.RoundToInt(_slider.value * 100)}%";
        
        if (_slider.value >= 0.99f)
        {
            _slider.value = 1f;
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
