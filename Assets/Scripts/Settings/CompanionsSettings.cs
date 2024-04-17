using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "HeioIDLE/Settings/CompanionsSettings", fileName = "CompanionsSettings")]
public class CompanionsSettings : ScriptableObject
{
    [field: SerializeField] public List<CompanionSetting> CompanionSettingsList { get; private set; }
    [field: SerializeField] public float LevelDamageMultiplier { get; private set; }
    [field: SerializeField] public float LevelAutoAttackMultiplier { get; private set; }

    public CompanionSetting GetEnemySetting(string id)
    {
        return CompanionSettingsList.First(x => x.Id == id);
    }

    public void AddCompanion(CompanionSetting companionSetting)
    {
        CompanionSettingsList.Add(companionSetting);
    }
}

public enum Rarity
{
    Common,
    Rare,
    Mythical,
    Legendary
}