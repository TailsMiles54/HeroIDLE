using System;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BattleManager : MonoSingleton<BattleManager>
{
    [field: SerializeField] public PlayerInfoPanel PlayerInfoPanel;
    [field: SerializeField] public EnemyInfoPanel EnemyInfoPanel;
    
    [field: SerializeField] public PlayerController PlayerController { get; private set; }
    private EnemyController _enemyController;

    public event Action<EnemySetting.EnemyType> EnemyKilled; 

    private bool _pause;
    [SerializeField] private Image _buttonImage;
    [SerializeField] private Sprite _playSprite;
    [SerializeField] private Sprite _pauseSprite;
    [SerializeField] private ParticleSystem _particleCritical;

    public void SetupEnemy(EnemyController enemyController)
    {
        _enemyController = enemyController;
    }

    public void DamageEnemy(float damage)
    {
        if(_pause)
            return;
        
        var criticalChance = PlayerController.Instance.GetUpgradeValue(UpgradeSetting.UpgradeType.CriticalChance);
        var criticalDamage = PlayerController.Instance.GetUpgradeValue(UpgradeSetting.UpgradeType.CriticalDamage);
        
        if(Random.Range(0,100f) <= criticalChance)
        {
            var critical = criticalDamage/100+1; 
            damage *= critical;
            _particleCritical.Play();            
        }
        
        var health = _enemyController.TakeDamage(damage);
        EnemyInfoPanel.UpdateHealthBar(health, _enemyController.EnemySetting.Health);

        FlyingTextController.Instance.ShowText(Color.red, _enemyController.transform.parent, $"-{Math.Round(damage, 2)}");
        if (health <= 0)
        {
            GetReward(_enemyController.EnemySetting);
            AppMetrica.Instance.ReportEvent("KillEnemy", _enemyController.EnemySetting.Name);
            EnemyKilled?.Invoke(_enemyController.EnemySetting.Type);
            WaveManager.Instance.NextEnemy();
        }
    }

    private void GetReward(EnemySetting enemySetting)
    {
        var moneyReward = PlayerController.Instance.GetUpgradeValue(UpgradeSetting.UpgradeType.MoneyBonus);

        var bonusMoney = enemySetting.MoneyReward * (moneyReward / 100 + 1);
        var newMoneyReward = Mathf.RoundToInt(bonusMoney); 
        
        PlayerController.AddReward(newMoneyReward, enemySetting.ScoreReward);
        PlayerInfoPanel.UpdatePanel(PlayerController.Instance);
        SaveManager.Instance.Save(PlayerController.Instance);
    }

    public void DamagePlayer(int damage)
    {
        var health = PlayerController.TakeDamage(damage);
        PlayerInfoPanel.UpdatePanel(PlayerController);
        FlyingTextController.Instance.ShowText(Color.red, PlayerController.transform.parent, $"-{damage}");

        if (health <= 0)
        {
            AppMetrica.Instance.ReportEvent("Dead");
            WaveManager.Instance.GoToFirstEnemy();
            PlayerController.Instance.ResetHealth();
        }
    }

    public void ChangeGameState()
    {
        if (!_pause)
        {
            _buttonImage.sprite = _playSprite;
            Time.timeScale = 0;
            _pause = true;
        }
        else
        {
            _buttonImage.sprite = _pauseSprite;
            Time.timeScale = 1;
            _pause = false;
        }
    }
}