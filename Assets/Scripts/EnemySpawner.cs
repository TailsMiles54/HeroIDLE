using System;
using BlackTailsUnityTools.Editor;
using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [field: SerializeField] public Transform EnemyParent { get; private set; }
    [field: SerializeField] public TMP_Text NextEnemyText { get; private set; }
    
    public static EnemySpawner Instance { get; private set; }

    public EnemyController CurrentEnemyObject { get; private set; }
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
        if (CurrentEnemyObject != null)
        {
            Destroy(CurrentEnemyObject.gameObject);
        }
        
        EnemySetting newEnemySetting = SettingsProvider.Get<EnemiesSettings>().GetEnemySetting(WaveSetting.WaveEnemyList[_currentWaveStep]);
        
        CurrentEnemyObject = Instantiate(newEnemySetting.EnemyController, EnemyParent);
        var enemyController = CurrentEnemyObject.GetComponent<EnemyController>();
        enemyController.Setup(newEnemySetting);
        BattleManager.Instance.SetupEnemy(enemyController);
        
        BattleManager.Instance.EnemyInfoPanel.UpdatePanel(newEnemySetting);
    }

    public void NextStep()
    {
        SpawnEnemy();
    }

    public void NextEnemy()
    {
        _currentWaveStep = Math.Clamp(_currentWaveStep + 1, 0, WaveSetting.WaveEnemyList.Count-1);
        Debug.Log(_currentWaveStep);
        NextEnemyText.text = "Следующий враг: " + SettingsProvider.Get<EnemiesSettings>()
            .GetEnemySetting(WaveSetting.WaveEnemyList[_currentWaveStep]).Name;
    }

    public void PreviousEnemy()
    {
        _currentWaveStep = Math.Clamp(_currentWaveStep - 1, 0, WaveSetting.WaveEnemyList.Count-1);
        Debug.Log(_currentWaveStep);
        NextEnemyText.text = "Следующий враг: " + SettingsProvider.Get<EnemiesSettings>()
            .GetEnemySetting(WaveSetting.WaveEnemyList[_currentWaveStep]).Name;
    }

    public void GoToFirstEnemy()
    {
        _currentWaveStep = 0;
    }
}
