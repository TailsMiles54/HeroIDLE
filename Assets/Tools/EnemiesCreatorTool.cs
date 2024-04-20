using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using SettingsProvider = BlackTailsUnityTools.Editor.SettingsProvider;

public class EnemiesCreatorTool
{
    #if UNITY_EDITOR
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
    
    [MenuItem("BlackTailsTools/Повысить ХП в 2 раза и урон в 1.5")]
    static void UpEnemies()
    {
        var npcSettings = SettingsProvider.Get<EnemiesSettings>();
        var enemiesSettings = npcSettings.EnemiesSettingsList;

        foreach (var enemySetting in enemiesSettings)
        {
            // enemySetting.DamageUp((int)(enemySetting.Damage * 1.5f));
            // enemySetting.HealthUp(enemySetting.Health * 2);
            EditorUtility.SetDirty(enemySetting);
        }
    }
    #endif
}


public class MainQuestCreatorTool
{
#if UNITY_EDITOR
    [MenuItem("BlackTailsTools/Создать основные квесты")]
    static void CreateEnemies()
    {
        var questsSettings = SettingsProvider.Get<QuestsSettings>();
        var enemiesTypes = Enum.GetValues(typeof(EnemySetting.EnemyType)).Cast<EnemySetting.EnemyType>().ToList();

        foreach (var enemyType in enemiesTypes)
        {
            var index = enemiesTypes.IndexOf(enemyType);
            QuestSetting questSetting = ScriptableObject.CreateInstance<QuestSetting>();
            questSetting.Init($"main_{index}", "Необходимо убить {0} {1}. Потом сможешь идти дальше.", enemyType, $"main_{index - 1}");
            AssetDatabase.CreateAsset(questSetting, $"Assets/Settings/Quests/Main/main_{index}.asset");
            questsSettings.AddQuest(questSetting);

            Debug.Log($"main_{index}.asset created");
        }
    }
#endif
}