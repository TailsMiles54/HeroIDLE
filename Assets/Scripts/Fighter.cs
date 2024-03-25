using System;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    public AnimationController AnimationController;
    [field: SerializeField] public float Health { get; protected set; }

    private void Awake()
    {
        if (AnimationController == null)
            AnimationController = GetComponent<AnimationController>();
    }

    public float TakeDamage(float damage)
    {
        Health -= damage;
        return Health;
    }
}