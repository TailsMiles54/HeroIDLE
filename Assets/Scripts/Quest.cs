using System;
using BlackTailsUnityTools.Editor;

public class Quest
{
    public string Id { get; private set; }
    public QuestSetting QuestSetting => SettingsProvider.Get<QuestsSettings>().GetQuestSetting(Id);
    public string Title => QuestSetting.Title;
    public string Description => QuestSetting.Description;
    public int Progress { get; private set; }
    
    public bool Completed { get; private set; }
    
    public event Action ProgressChanged;
    
    public Quest(string id)
    {
        Id = id;
        BattleManager.Instance.EnemyKilled += QuestCheck;
    }

    private void QuestCheck(EnemySetting.EnemyType enemyType)
    {
        if (QuestSetting.QuestType == QuestType.Hunt && QuestSetting.EnemyType == enemyType)
        {
            ProgressChanged?.Invoke();
            Progress++;
            if (Progress >= QuestSetting.TargetCount)
                QuestSystem.Instance.CompleteQuest(Id);
        }
    }

    public void Complete()
    {
        Completed = true;
        Reward();
    }

    public void Reward()
    {
        //todo add reward
    }
}