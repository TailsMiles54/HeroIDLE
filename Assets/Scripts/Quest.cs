using System;

public class Quest
{
    public string Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public QuestType Type { get; private set; }
    

    public Quest(string questTitle, string description, QuestType type)
    {
        Id = Guid.NewGuid().ToString();
        Title = questTitle;
        Description = description;
        Type = type;
    }
}

public enum QuestType
{
    Hunt = 0,
    Upgrade = 1,
    CompanionUpgrade = 2,
    ItemUpgrade = 3
}