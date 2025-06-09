using System.Collections.Generic;
using UnityEngine;

public class PlayerRecordSendManager : MonoSingleton<PlayerRecordSendManager>
{
    public string maxTime;
    public string maxScore;
    
    public void FixRecordMT(string MaxTime)
    {
        maxTime = MaxTime;
    }

    public void FixRecordMS(string MaxScore)
    {
        maxScore = MaxScore;
    }
    
}
