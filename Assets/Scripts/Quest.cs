using System;
using BlackTailsUnityTools.Editor;

public class Quest
{
    public string Id { get; private set; }
    public QuestSetting QuestSetting => SettingsProvider.Get<QuestsSettings>().GetQuestSetting(Id);
    public string Title => QuestSetting.Title;
    public string Description => QuestSetting.Description;
    
    public Quest(string id)
    {
        Id = id;
    }
}