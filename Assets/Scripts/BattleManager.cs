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
}