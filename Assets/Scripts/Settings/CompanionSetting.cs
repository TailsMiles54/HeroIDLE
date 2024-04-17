using System;
using UnityEngine;

[CreateAssetMenu(menuName = "HeioIDLE/Settings/CompanionSetting", fileName = "CompanionSetting")]
public class CompanionSetting : ScriptableObject
{
    [field: SerializeField] public string Id { get; private set; }
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public float AutoAttackTime { get; private set; }
    [field: SerializeField] public CompanionController EnemyController { get; private set; }
    [field: SerializeField] public Rarity Rarity { get; private set; }

    public void Init()
    {
        Id = Guid.NewGuid().ToString();
    }
}