using System;
using BlackTailsUnityTools.Editor;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [field: SerializeField] public Transform EnemyParent { get; private set; }
    
    public static EnemySpawner Instance { get; private set; }

    private EnemyController _currentEnemyObject;
    private WaveSetting WaveSetting => SettingsProvider.Get<WaveSetting>();
    private int _currentWaveStep = 0;
    
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
            Destroy(_currentEnemyObject.gameObject);
        }
        
        EnemySetting newEnemySetting = SettingsProvider.Get<EnemiesSettings>().GetEnemySetting(WaveSetting.WaveEnemyList[_currentWaveStep]);
        
        _currentEnemyObject = Instantiate(newEnemySetting.EnemyController, EnemyParent);
        var enemyController = _currentEnemyObject.GetComponent<EnemyController>();
        BattleManager.Instance.SetupEnemy(enemyController);
        
        BattleManager.Instance.EnemyInfoPanel.UpdatePanel(newEnemySetting);
    }

    public void NextWaveStep()
    {
        _currentWaveStep++;
    }
}
