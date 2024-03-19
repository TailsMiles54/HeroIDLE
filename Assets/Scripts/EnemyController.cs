using UnityEngine;

public class EnemyController : Fighter
{
    [field: SerializeField] public float Health { get; private set; }
    
    public void TakeDamage(float damage)
    {
        Health -= damage;
    }
}