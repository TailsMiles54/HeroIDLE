using System;
using System.Collections;
using System.Collections.Generic;
using BlackTailsUnityTools.Editor;
using UnityEngine;

public class WaveManager : MonoSingleton<WaveManager>
{
    [field: SerializeField] public Transform EnemyParent { get; private set; }
    [field: SerializeField] public ParticleSystem SpawnParticle { get; private set; }

    public EnemyController CurrentEnemyObject { get; private set; }
    private WavesSettings WavesSettings => SettingsProvider.Get<WavesSettings>();
    private WaveSetting CurrentWaveSetting => WavesSettings.Waves[CurrentWave];
    private WaveStep CurrentWaveStep => CurrentWaveSetting.WaveSteps[CurrentStep];

    private int _maxWave;
    public int CurrentWave { get; private set; } = 1;
    public int CurrentStep { get; private set; } = 1;
    public int CurrentEnemy { get; private set; } = 1;
    
    public void NextWave()
    {
        CurrentWave++;
        CurrentStep = 1;
    }
    
    public void NextStep()
    {
        CurrentStep = Math.Clamp(CurrentStep + 1, 0, CurrentWaveSetting.WaveSteps.Count-1);
        Debug.Log(CurrentStep);
    }
    
    
    public void SpawnEnemy()
    { 
        if (CurrentEnemyObject != null)
        {
            Destroy(CurrentEnemyObject.gameObject);
        }
        
        EnemySetting newEnemySetting = SettingsProvider.Get<EnemiesSettings>().GetEnemySetting(CurrentWaveStep.WaveEnemyList[CurrentEnemy]);
        
        CurrentEnemyObject = Instantiate(newEnemySetting.EnemyController, EnemyParent);
        SpawnParticle.Play();
        var enemyController = CurrentEnemyObject.GetComponent<EnemyController>();
        enemyController.Setup(newEnemySetting);
        BattleManager.Instance.SetupEnemy(enemyController);
        AppMetrica.Instance.ReportEvent("SpawnEnemy", newEnemySetting.Name);
        
        BattleManager.Instance.EnemyInfoPanel.UpdatePanel(newEnemySetting);
        QuestSystem.Instance.GenerateQuests();
    }

    public void GoToFirstEnemy()
    {
        CurrentStep = 0;
    }

    public void NextEnemy()
    {
        CurrentEnemy++;
        if (CurrentEnemy >= CurrentWaveStep.WaveEnemyList.Count)
        {
            CurrentEnemy = 0;
            NextStep();
        }
        SpawnEnemy();
    }
}
