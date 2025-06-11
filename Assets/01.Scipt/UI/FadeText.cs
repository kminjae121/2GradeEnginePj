using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class FadeText : MonoBehaviour
{
    public static FadeText Instance;

    [SerializeField] private List<TextMeshProUGUI> _txtList;

    private Dictionary<string, TextMeshProUGUI> _txt = new Dictionary<string, TextMeshProUGUI>();

    private Tween _fadeTween;
    private Tween _showTween;

    private void Awake()
    {
        Instance = this;
        
        _txtList.ForEach(txt =>_txt.Add(txt.transform.gameObject.name, txt));
    }

    public void FadeTxt(float endValue , float duration, string name)
    {
        if (_fadeTween != null && _fadeTween.IsActive())
            return;
        if (_fadeTween != null && _fadeTween.IsActive())
        {
            _fadeTween.Kill(true);
        }
        
        TextMeshProUGUI txt = _txt.GetValueOrDefault(name);
        _fadeTween = txt.DOFade(endValue, duration).OnComplete(()
            => txt.gameObject.SetActive(false));
    }

    public TextMeshProUGUI FindTxt(string name)
    {
        return _txt.GetValueOrDefault(name);
    }

    public void ShowTxt(float endValue, float duration,string name)
    {
        if (_showTween != null && _showTween.IsActive())
            return;
        if (_showTween != null && _showTween.IsActive())
        {
            _showTween.Kill(true);
        }
        TextMeshProUGUI txt = _txt.GetValueOrDefault(name);
        _showTween = txt.DOFade(endValue, duration);
    }
}
