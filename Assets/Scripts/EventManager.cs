using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager
{
    public static event Action<float> EnemyDestroyed;

    public static void TriggerEnemyDestroyed(float scoreValue)
    {
        EnemyDestroyed?.Invoke(scoreValue);
    }
}
