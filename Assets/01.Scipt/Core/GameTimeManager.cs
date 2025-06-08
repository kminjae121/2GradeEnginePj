using System;
using UnityEngine;

public class GameTimeManager : MonoSingleton<GameTimeManager>
{

    public float Gametime { get; set; } = 0;

    public float maxTime { get; set; } = 0;

    public void TickTime()
    {
        Gametime += Time.deltaTime;
    }

    public void overrideTime()
    {
        if (Gametime > maxTime)
        {
            maxTime = Gametime;
        }
    }
    
}
