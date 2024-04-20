using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "HeioIDLE/Settings/QuestsSettings", fileName = "QuestsSettings")]
public class QuestsSettings : ScriptableObject
{
    [field: SerializeField] public List<QuestSetting> QuestSettings { get; private set; }
    
    public QuestSetting GetQuestSetting(string id) => QuestSettings.First(x => x.Id == id);

    public void AddQuest(QuestSetting questSetting)
    {
        QuestSettings.Add(questSetting);
    }
}

[Serializable]
public class QuestPanelAppearanceSetting
{
    public QuestType QuestType;
    public Color32 Color;
}