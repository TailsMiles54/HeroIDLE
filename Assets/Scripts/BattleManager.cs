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
        PlayerController.AddReward(enemySetting.MoneyReward, 1);
        PlayerInfoPanel.UpdatePanel(PlayerController.Instance);
    }

    public void DamagePlayer(int damage)
    {
        var health = PlayerController.TakeDamage(damage);
        PlayerInfoPanel.UpdatePanel(PlayerController);

        if (health <= 0)
        {
            PopupSystem.Instance.ShowPopup(new DeathPopupSettings()
            {
                
            });
        }
    }
}