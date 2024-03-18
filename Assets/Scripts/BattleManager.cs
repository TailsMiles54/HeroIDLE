using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [field: SerializeField] public PlayerInfoPanel PlayerInfoPanel;
    [field: SerializeField] public EnemyInfoPanel EnemyInfoPanel;
    
    public static BattleManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
}
