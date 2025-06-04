using DG.Tweening;
using UnityEngine;

public class CameraShakingManager : MonoBehaviour
{
    public static CameraShakingManager instance;

    public Transform _camPos;

    private Vector3 _originalPos;
    private Tween _shakeTween;

    private void Awake()
    {
        instance = this;
        _originalPos = _camPos.localPosition;
    }

    public void ShakeCam(float duration, float strength, int vibrato, float randomness)
    {
        if (_shakeTween != null && _shakeTween.IsActive())
            return;
        if (_shakeTween != null && _shakeTween.IsActive())
        {
            _shakeTween.Kill(true);
            _camPos.localPosition = _originalPos;
        }
        
        _shakeTween = _camPos.DOShakePosition(duration, strength, vibrato, randomness)
            .OnComplete(() => {
                _camPos.localPosition = _originalPos;
            });
    }
}