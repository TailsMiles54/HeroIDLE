using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyInfoPanel : MonoBehaviour
{
    [field: SerializeField] public TMP_Text Title { get; private set; }
    [field: SerializeField] public Scrollbar HealthBar { get; private set; }
    
    public void UpdatePanel(EnemySetting enemySetting)
    {
        Title.text = enemySetting.Type.ToString();
    }
}