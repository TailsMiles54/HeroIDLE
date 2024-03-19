using UnityEngine;

public class Fighter : MonoBehaviour
{
    public AnimationController AnimationController;
    [field: SerializeField] public float Health { get; protected set; }
    
    public float TakeDamage(float damage)
    {
        Health -= damage;
        return Health;
    }
}