using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "HeioIDLE/Settings/EnemiesSettings", fileName = "EnemiesSettings")]
public class EnemiesSettings : ScriptableObject
{
    [field: SerializeField] public List<EnemySetting> EnemiesSettingsList { get; private set; }

    public EnemySetting GetEnemySetting(EnemySetting.EnemyType enemyType)
    {
        return EnemiesSettingsList.First(x => x.Type == enemyType);
    }

    public void AddEnemy(EnemySetting enemySetting)
    {
        EnemiesSettingsList.Add(enemySetting);
    }
}