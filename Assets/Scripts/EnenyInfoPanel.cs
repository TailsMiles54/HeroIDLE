using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyInfoPanel : MonoBehaviour
{
    [field: SerializeField] public TMP_Text Title { get; private set; }
    [field: SerializeField] public TMP_Text Health { get; private set; }
    [field: SerializeField] public LandedProgressBar LandedProgressBarHealth { get; private set; }
    [field: SerializeField] public LandedProgressBar LandedProgressBarAutoAttackTime { get; private set; }
    
    public void UpdatePanel(EnemySetting enemySetting)
    {
        Title.text = enemySetting.Name;
        UpdateHealthBar(enemySetting.Health, enemySetting.Health);
    }

    public void UpdateHealthBar(float current, float maxHealth)
    {
        Health.text = $"{(int)current}/{(int)maxHealth}";
        var value = current / maxHealth;
        LandedProgressBarHealth.UpdateProgress(value);
    }
    
    public void UpdateAutoAttackTimeBar(float time)
    {
        LandedProgressBarAutoAttackTime.ToMinWithTime(time);
    }
}
