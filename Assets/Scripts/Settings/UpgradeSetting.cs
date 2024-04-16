using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "HeioIDLE/Settings/UpgradeSetting", fileName = "UpgradeSetting")]
public class UpgradeSetting : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public UpgradeType Type { get; private set; }
    [field: SerializeField] public List<UpgradeLevel> Value { get; private set; }
    
    [Serializable]
    public struct UpgradeLevel
    {
        public int Cost;
        public float Value;
    }
    
    public enum UpgradeType
    {
        Damage,
        Health,
        AutoAttackSpeed,
        MoneyBonus,
        CriticalDamage,
        CriticalChance,
        Heal,
        ClickDamage
    }
}