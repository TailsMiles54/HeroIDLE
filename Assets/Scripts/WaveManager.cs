using System;
using System.Collections;
using System.Collections.Generic;
using BlackTailsUnityTools.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoSingleton<WaveManager>
{
    [field: SerializeField] public Transform EnemyParent { get; private set; }
    [field: SerializeField] public ParticleSystem SpawnParticle { get; private set; }
    [field: SerializeField] public Scrollbar WaveScrollbar { get; private set; }
    [field: SerializeField] public TMP_Text WaveText { get; private set; }

    public EnemyController CurrentEnemyObject { get; private set; }
    private WavesSettings WavesSettings => SettingsProvider.Get<WavesSettings>();
    private WaveSetting CurrentWaveSetting => WavesSettings.Waves[CurrentWave];
    private WaveStep CurrentWaveStep => CurrentWaveSetting.WaveSteps[CurrentStep];

    private int _maxWave;
    public int CurrentWave { get; private set; } = 0;
    public int CurrentStep { get; private set; } = 0;
    public int CurrentEnemy { get; private set; } = 0;
    
    public void NextWave()
    {
        CurrentWave++;
        CurrentStep = 0;
    }

    public void NextStep()
    {
        if (CurrentStep + 1 >= CurrentWaveSetting.WaveSteps.Count)
        {
            NextWave();
        }
        else
        {
            CurrentStep = Math.Clamp(CurrentStep + 1, 0, CurrentWaveSetting.WaveSteps.Count-1);
        }
        
        Debug.Log(CurrentStep);
    }

    private void Start()
    {
        SpawnEnemy();
    }

    public void LoadData(WavesProgressSaveData wavesProgressSaveData)
    {
        CurrentWave = wavesProgressSaveData.CurrentWave;
        CurrentStep = wavesProgressSaveData.CurrentStep;
        CurrentEnemy = wavesProgressSaveData.CurrentEnemy;
    }

    public void SpawnEnemy()
    { 
        WaveScrollbar.numberOfSteps = CurrentWaveSetting.WaveSteps.Count-1;
        WaveScrollbar.value = (float)CurrentStep / CurrentWaveSetting.WaveSteps.Count;
        WaveText.text = $"Wave: {CurrentWave+1} Current Step: {CurrentStep+1}";

        SaveManager.Instance.SaveWavesProgress(CurrentWave, CurrentStep, CurrentEnemy);
        
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
        CurrentEnemy = 0;
        SpawnEnemy();
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
