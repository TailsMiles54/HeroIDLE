using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using SettingsProvider = BlackTailsUnityTools.Editor.SettingsProvider;

public class EnemiesCreatorTool
{
    [MenuItem("BlackTailsTools/Создать объекты противников")]
    static void CreateEnemies()
    {
        var npcSettings = SettingsProvider.Get<EnemiesSettings>();
        var enemiesSettings = npcSettings.EnemiesSettingsList;
        var addedEnemies = enemiesSettings.Select(x => x.Type);
        var enemiesTypes = Enum.GetValues(typeof(EnemySetting.EnemyType)).Cast<EnemySetting.EnemyType>().ToList();
        var newEnemies = enemiesTypes.Except(addedEnemies).ToList();
        
        foreach (var enemyType in newEnemies)
        {
            EnemySetting enemySetting = ScriptableObject.CreateInstance<EnemySetting>();
            enemySetting.Init(enemyType);
            AssetDatabase.CreateAsset(enemySetting, $"Assets/Settings/Enemies/{enemyType.ToString()}.asset");
            npcSettings.AddEnemy(enemySetting);
            
            Debug.Log($"{enemyType.ToString()}.asset created");
        }
    }
}
