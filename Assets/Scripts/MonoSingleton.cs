using System;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : Component
{
    public static T Instance { get; private set; }
    
    protected virtual void Awake()
    {
        if (Instance != null && Instance.transform != transform)
        {
            throw new Exception($"Duplicated");
        }

        if (Instance != null)
            return;
        
        Instance = GetComponent<T>();
        DontDestroyOnLoad(gameObject);
    }
}