using UnityEngine;

public class EnemyController : Fighter
{
    [field: SerializeField] public EnemySetting EnemySetting { get; private set; }

    public void Setup(EnemySetting enemySetting)
    {
        EnemySetting = enemySetting; 
        Health = enemySetting.Health;
    }
}