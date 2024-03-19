using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "HeioIDLE/Settings/UpgradesSettings", fileName = "UpgradesSettings")]
public class UpgradesSettings : ScriptableObject
{
    [field: SerializeField] public List<UpgradeSetting> Upgrades { get; private set; }
    
    public float GetBonusValue(UpgradeSetting.UpgradeType upgradeType, int level) =>
        Upgrades.First(x => x.Type == upgradeType).Value[level].Value;
}