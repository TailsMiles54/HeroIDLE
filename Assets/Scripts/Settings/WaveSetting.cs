using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "HeioIDLE/Settings/WaveSetting", fileName = "WaveSetting")]
public class WaveSetting : ScriptableObject
{
    [field: SerializeField]
    public List<EnemySetting.EnemyType> WaveEnemyList { get; private set; } = new List<EnemySetting.EnemyType>()
    {
        EnemySetting.EnemyType.Bat,
        EnemySetting.EnemyType.Dragon,
        EnemySetting.EnemyType.EvilMage,
        EnemySetting.EnemyType.Golem,
        EnemySetting.EnemyType.Plant,
        EnemySetting.EnemyType.Orc,
        EnemySetting.EnemyType.Skeleton,
        EnemySetting.EnemyType.Slime,
        EnemySetting.EnemyType.Spider,
        EnemySetting.EnemyType.Turtle,
        EnemySetting.EnemyType.Beholder,
        EnemySetting.EnemyType.BlackKnight,
        EnemySetting.EnemyType.ChestMonster,
        EnemySetting.EnemyType.CrabMonster,
        EnemySetting.EnemyType.FylingDemon,
        EnemySetting.EnemyType.LizardWarrior,
        EnemySetting.EnemyType.RatAssassin,
        EnemySetting.EnemyType.Specter,
        EnemySetting.EnemyType.Werewolf,
        EnemySetting.EnemyType.WormMonster,
        EnemySetting.EnemyType.BattleBee,
        EnemySetting.EnemyType.BishopKnight,
        EnemySetting.EnemyType.Cactus,
        EnemySetting.EnemyType.Cyclops,
        EnemySetting.EnemyType.DemonKing,
        EnemySetting.EnemyType.Fishman,
        EnemySetting.EnemyType.MushroomAngry,
        EnemySetting.EnemyType.MushroomSmile,
        EnemySetting.EnemyType.NagaWizard,
        EnemySetting.EnemyType.Salamander,
        EnemySetting.EnemyType.StingRay,
    };

}