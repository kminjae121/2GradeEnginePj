using System;
using DG.Tweening;
using UnityEngine;

public class CameraShakingManager : MonoBehaviour
{
    public static CameraShakingManager instance;

    public Transform _camPos;

    private void Awake()
    {
        instance = this;
    }

    public void ShakeCam(float duration,float strength, int vibrato,float randomness)
    {
        _camPos.DOShakePosition(duration,strength,vibrato,randomness);
    }
}
