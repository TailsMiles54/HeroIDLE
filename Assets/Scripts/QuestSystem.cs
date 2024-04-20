using System.Linq;
using BlackTailsUnityTools.Editor;

public class QuestSystem : MonoSingleton<QuestSystem>
{
    private PlayerController PlayerController => PlayerController.Instance;

    private void Start()
    {
        GenerateQuests();
    }

    public void GenerateQuests()
    {
        var quests = SettingsProvider.Get<QuestsSettings>().QuestSettings.Where(x => x.Available());

        foreach (var questSetting in quests)
        {
            var quest = new Quest(questSetting.Id);
            PlayerController.Quests.Add(quest);
        }
    }

    public void CompleteQuest(string questId)
    {
        var quest = PlayerController.Quests.FirstOrDefault(x => x.Id == questId);
        if (quest!= null)
        {
            quest.Complete();
        }
        GenerateQuests();
    }
}
