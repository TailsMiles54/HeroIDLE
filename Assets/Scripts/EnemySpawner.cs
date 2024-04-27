using System;
using System.Linq;
using BlackTailsUnityTools.Editor;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;

public class EnemySpawner : MonoSingleton<EnemySpawner>
{
    [field: SerializeField] public Transform EnemyParent { get; private set; }
    [field: SerializeField] public TMP_Text NextEnemyText { get; private set; }
    [field: SerializeField] public ParticleSystem SpawnParticle { get; private set; }

    public EnemyController CurrentEnemyObject { get; private set; }
    private WaveSetting WaveSetting => SettingsProvider.Get<WaveSetting>();
    private int _currentWaveStep = 0;

    private int _maxWave;

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
        
        EnemySetting newEnemySetting = SettingsProvider.Get<EnemiesSettings>().GetEnemySetting(WaveSetting.WaveSteps[0].WaveEnemyList[_currentWaveStep]);
        
        CurrentEnemyObject = Instantiate(newEnemySetting.EnemyController, EnemyParent);
        SpawnParticle.Play();
        var enemyController = CurrentEnemyObject.GetComponent<EnemyController>();
        enemyController.Setup(newEnemySetting);
        BattleManager.Instance.SetupEnemy(enemyController);
        AppMetrica.Instance.ReportEvent("SpawnEnemy", newEnemySetting.Name);
        
        BattleManager.Instance.EnemyInfoPanel.UpdatePanel(newEnemySetting);
        QuestSystem.Instance.GenerateQuests();
    }

    public void NextStep()
    {
        SpawnEnemy();
    }

    public void NextEnemy()
    {
        _currentWaveStep = Math.Clamp(_currentWaveStep + 1, 0, WaveSetting.WaveSteps[0].WaveEnemyList.Count-1);
        Debug.Log(_currentWaveStep);
    }

    public void PreviousEnemy()
    {
        _currentWaveStep = Math.Clamp(_currentWaveStep - 1, 0, WaveSetting.WaveSteps[0].WaveEnemyList.Count-1);
        Debug.Log(_currentWaveStep);
    }

    public void GoToFirstEnemy()
    {
        _currentWaveStep = 0;
    }
}
