using System;
using UnityEngine;

[CreateAssetMenu(fileName = "StageSO", menuName = "SO/StageSO")]
public class StageSO : ScriptableObject
{
    public string stageName = string.Empty;

    public bool _isClearStage = false;

    private void OnValidate()
    {
        stageName = this.name;
    }
}
