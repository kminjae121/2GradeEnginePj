using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class StageManager : MonoSingleton<StageManager>
{
    [SerializeField] private List<StageSO> _stages;
    
    private Dictionary<string, StageSO> _stageDictionary = new Dictionary<string, StageSO>();

    protected override void Awake()
    {
        base.Awake();
        _stages.ForEach(stage => _stageDictionary.Add(stage.name, stage));
    }

    public void ClearStage(string stageName)
    {
        //_stageDictionary.GetValueOrDefault(stageName)._isClearStage = true;
    }
}
