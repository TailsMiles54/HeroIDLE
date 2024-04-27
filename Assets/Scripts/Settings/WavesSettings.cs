using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "HeioIDLE/Settings/WavesSettings", fileName = "WavesSettings")]
public class WavesSettings : ScriptableObject
{
    [field: SerializeField]
    public List<EnemySetting.EnemyType> WaveEnemyList { get; private set; } = new List<EnemySetting.EnemyType>() { };
}