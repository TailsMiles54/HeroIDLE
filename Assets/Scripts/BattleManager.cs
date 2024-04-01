using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [field: SerializeField] public PlayerInfoPanel PlayerInfoPanel;
    [field: SerializeField] public EnemyInfoPanel EnemyInfoPanel;
    
    [field: SerializeField] public PlayerController PlayerController { get; private set; }
    private EnemyController _enemyController;
    
    public static BattleManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    public void SetupEnemy(EnemyController enemyController)
    {
        _enemyController = enemyController;
    }

    public void NextEnemy()
    {
        EnemySpawner.Instance.NextStep();
    }

    public void DamageEnemy(float damage)
    {
        var criticalChance = PlayerController.Instance.GetUpgradeValue(UpgradeSetting.UpgradeType.CriticalChance);
        var criticalDamage = PlayerController.Instance.GetUpgradeValue(UpgradeSetting.UpgradeType.CriticalDamage);
        
        if(Random.Range(0,100f) <= criticalChance)
        {
            var critical = criticalDamage/100+1; 
            damage *= critical;
        }
        
        var health = _enemyController.TakeDamage(damage);
        EnemyInfoPanel.UpdateHealthBar(health, _enemyController.EnemySetting.Health);

        if (health <= 0)
        {
            GetReward(_enemyController.EnemySetting);
            EnemySpawner.Instance.NextStep();
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

        if (health <= 0)
        {
            PopupSystem.Instance.ShowPopup(new DeathPopupSettings()
            {
                Score = PlayerController.Instance.Score
            });
        }
    }
}