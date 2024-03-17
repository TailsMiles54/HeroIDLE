using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "HeioIDLE/Settings/UpgradesSettings", fileName = "UpgradesSettings")]
public class UpgradesSettings : ScriptableObject
{
    [field: SerializeField] public List<UpgradeSetting> Upgrades { get; private set; }
}