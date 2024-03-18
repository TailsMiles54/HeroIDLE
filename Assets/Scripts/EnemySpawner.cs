using System;
using BlackTailsUnityTools.Editor;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [field: SerializeField] public Transform EnemyParent { get; private set; }
    
    public static EnemySpawner Instance { get; private set; }

    private EnemySetting.EnemyType _nextEnemyType = EnemySetting.EnemyType.Bat;

    private GameObject _currentEnemyObject;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        if (_currentEnemyObject != null)
        {
            Destroy(_currentEnemyObject);
        }
        
        EnemySetting newEnemySetting = SettingsProvider.Get<EnemiesSettings>().GetEnemySetting(_nextEnemyType);
        
        _currentEnemyObject = Instantiate(newEnemySetting.Prefab, EnemyParent);
        
        BattleManager.Instance.EnemyInfoPanel.UpdatePanel(newEnemySetting);
    }
}
