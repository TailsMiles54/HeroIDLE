using UnityEngine;

[CreateAssetMenu(menuName = "HeioIDLE/Settings/EnemySetting", fileName = "EnemySetting")]
public class EnemySetting : ScriptableObject
{
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public int Health { get; private set; }
    [field: SerializeField] public int MoneyReward { get; private set; }
    [field: SerializeField] public int ScoreReward { get; private set; }
    [field: SerializeField] public GameObject Prefab { get; private set; }
    
    public enum EnemyType
    {
        Bat,
    }
}