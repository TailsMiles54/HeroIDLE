using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "HeioIDLE/Settings/WaveSetting", fileName = "WaveSetting")]
public class WaveSetting : ScriptableObject
{
    [field: SerializeField]
    public List<WaveStep> WaveSteps { get; private set; } = new List<WaveStep>();
}

[Serializable]
public class WaveStep
{
    public List<EnemySetting.EnemyType> WaveEnemyList = new List<EnemySetting.EnemyType>();
}