using UnityEngine;

[CreateAssetMenu(menuName = "HeioIDLE/Settings/EnemySetting", fileName = "EnemySetting")]
public class EnemySetting : ScriptableObject
{
    [field: SerializeField] public EnemyType Type { get; private set; }
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public int Health { get; private set; }
    [field: SerializeField] public int MoneyReward { get; private set; }
    [field: SerializeField] public int ScoreReward { get; private set; }
    [field: SerializeField] public GameObject Prefab { get; private set; }
    
    public enum EnemyType
    {
        Bat,
        Dragon,
        EvilMage,
        Golem,
        Plant,
        Orc,
        Skeleton,
        Slime,
        Spider,
        Turtle,
        
        Beholder,
        BlackKnight,
        ChestMonster,
        CrabMonster,
        FylingDemon,
        LizardWarrior,
        RatAssassin,
        Specter,
        Werewolf,
        WormMonster,
        
        BattleBee,
        BishopKnight,
        Cactus,
        Cyclops,
        DemonKing,
        Fishman,
        MushroomAngry,
        MushroomSmile,
        NagaWizard,
        Salamander,
        StingRay
    }
}