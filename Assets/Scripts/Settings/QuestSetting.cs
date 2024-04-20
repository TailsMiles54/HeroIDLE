using System;
using System.Collections.Generic;
using BlackTailsUnityTools.Editor;
using UnityEngine;

[CreateAssetMenu(menuName = "HeioIDLE/Settings/QuestSetting", fileName = "QuestSetting")]
public class QuestSetting : ScriptableObject
{
    [field: SerializeField] public string Id { get; private set; }
    [field: SerializeField] public string Title { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    

    [field: Space(15)] 
    [field: Header("Предыдущий квест")] 
    [field: SerializeField] public string PreviousQuestId { get; private set; }
    [field: Space(15)] 
    
    [field: SerializeField] public QuestLine QuestLine { get; private set; }
    [field: SerializeField] public QuestType QuestType { get; private set; }
    [field: SerializeField] public QuestReward QuestReward { get; private set; }

    [field: Space(15)] 
    [field: Header("Hunt settings")] 
    [field: SerializeField] public EnemySetting.EnemyType EnemyType { get; private set; }
    [field: SerializeField] public int TargetCount { get; private set; }

    public void Init(string id, string desc, EnemySetting.EnemyType enemyType, string previousQuestId)
    {
        var enemySetting = SettingsProvider.Get<EnemiesSettings>().GetEnemySetting(enemyType);
        Id = id;
        PreviousQuestId = previousQuestId;
        TargetCount = 10;
        Description = string.Format(desc, TargetCount, enemySetting.Name);
        EnemyType = enemyType;
    }
}

[Serializable]
public class QuestReward
{
    public int GoldReward;
    public List<ItemReward> ItemRewards;
}

[Serializable]
public class ItemReward
{
    public ItemType ItemType;
    public int Count;
}

public enum QuestType
{
    Hunt = 0,
    Upgrade = 1,
    CompanionUpgrade = 2,
    ItemUpgrade = 3
}

public enum QuestLine
{
    Main = 0,
    Secondary = 1,
}